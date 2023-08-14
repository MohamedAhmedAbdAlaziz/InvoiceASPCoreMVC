
using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Dtos.ViewModels
{
    public class  InvoiceViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Code.")]
        public int Code { get; set; }
        [Required(ErrorMessage = "Please enter Date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "Please enter Date.")]
        public int customerid { get; set; }
        public List<ITemViewModel> ItemViewModels { get; set; }= new List<ITemViewModel>();
        public decimal Amount { get; set; }

    }
}
