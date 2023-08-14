using AutoMapper;
using EcommerceProject.Dtos;
using Core.Models.Entities;
using Core.Models.Interfaces;
using Core.Models.Specifications;
using Microsoft.AspNetCore.Mvc;
using cloudscribe.Pagination.Models;
using EcommerceProject.Dtos.ViewModels;

namespace EcommerceProject.Controllers
{
    public class PurchaseController : Controller
    {
         
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseController(IUnitOfWork unitOfWork, IMapper mapper 
           )
        {   _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int pagenumber = 1, int pageSize=4)
        {
            var InvoiceSpecParams= new InvoiceSpecParams() { 
             PageIndex = pagenumber,
             PageSize = pageSize
            };
          // get all Invoices add apply Pagination and send data to the view 
            var items = await _unitOfWork.Repository<Invoice>().GetAllAsync();
            int itemsCount= items.Count;
            var Spec = new InvoiceWithCustomerSpecification(InvoiceSpecParams);
            var Invoices = await _unitOfWork.Repository<Invoice>().ListAsync(Spec);
            var Invoiceslist = _mapper.Map<IReadOnlyList<Invoice>, IReadOnlyList<InvoiceDto>>(Invoices);
            var result = new PagedResult<InvoiceDto>
            {
                Data = Invoiceslist.ToList(),
                TotalItems= itemsCount,
                PageNumber= pagenumber,
                PageSize=pageSize

            };
            return View(result);
        }
        // 
        [HttpGet]
        public async Task<IActionResult> InvoiceAddOrEdit(int id = 0)
        {
            //get  the Customers and store it in ViewBag Data Transfer
            ViewBag.Customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
            if (id > 0)
            {
                Invoice invoices = await _unitOfWork.Repository<Invoice>().GetByIdAsync(id);
                var itemsWithSpecification = new ItemsWithSpecification(id);
                var items = await _unitOfWork.Repository<Item>().ListAsync(itemsWithSpecification);

                var model = new InvoiceViewModel()
                {
                    Id = id,
                    Code = invoices.Code,
                    Date = invoices.Date,
                    customerid = invoices.CustomerId,
                    ItemViewModels = _mapper.Map<List<Item>, List<ITemViewModel>>(items),
                    Amount=invoices.Amount
            };
            return View(model);         
            }
            else
            {
                return View(new InvoiceViewModel());
            }
     }
      

        [HttpPost]
        public async Task<JsonResult> InvoiceAddOrEdit(InvoiceViewModel invoiceViewModel)
        {
           
            if (ModelState.IsValid)
            {
                var total = invoiceViewModel.ItemViewModels.Select(x => x.Price * x.Quantity).Aggregate((sum, val) => (sum + val));
                var invoice = new Invoice
                {

                    Code = invoiceViewModel.Code,
                    Date = invoiceViewModel.Date,
                    CustomerId = invoiceViewModel.customerid,
                    Amount = total
                };
                if (invoiceViewModel.Id != 0)
                {
                 
                  var item = await _unitOfWork.Repository<Invoice>().GetByIdAsync(invoiceViewModel.Id);
                    _unitOfWork.Repository<Invoice>().Delete(item);
                      await _unitOfWork.Complete();
                }

                _unitOfWork.Repository<Invoice>().Add(invoice);
                var result = await _unitOfWork.Complete();
                if (result <= 0)
                {
                    ModelState.AddModelError("", "Code is token");
                    return Json(false);
                }              
                    var Spec = new InvoiceWithCustomerSpecification(invoice.Code);
                    var Invoicespec = await _unitOfWork.Repository<Invoice>().GetEntityWithSpec(Spec);
                    int  id = Invoicespec.Id;
                
                
                foreach (var item in invoiceViewModel.ItemViewModels)
                {
                    var newitem = new Item()
                    {
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        InvoiceId = id
                    };
                    _unitOfWork.Repository<Item>().Add(newitem);
                }
                var insertItemresalut = await _unitOfWork.Complete();
                if(insertItemresalut > 0)
                    return Json(true);
            }
            return Json(false);
        }
        //Delete The Invice by it's ID
        [HttpPost]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            
            var item = await _unitOfWork.Repository<Invoice>().GetByIdAsync(id);
            _unitOfWork.Repository<Invoice>().Delete(item);
            var result = await _unitOfWork.Complete();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
