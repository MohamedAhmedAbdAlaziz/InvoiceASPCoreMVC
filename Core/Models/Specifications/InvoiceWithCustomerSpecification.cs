using Core.Models.Entities;
using System.Linq.Expressions;

namespace Core.Models.Specifications
{
    public class InvoiceWithCustomerSpecification: BaseSpecification<Invoice>
    {
        public InvoiceWithCustomerSpecification(int code) : base(x=>x.Code==code)
        {
            AddInclude(x => x.Customer);
            AddOrderByDescending(x => x.Date);

        }
        public InvoiceWithCustomerSpecification():base()
        {
            AddInclude(x => x.Customer);
            AddOrderByDescending(x => x.Date);

        }

        public InvoiceWithCustomerSpecification(InvoiceSpecParams invoiceSpecParams) : base()
        {
            AddInclude(x => x.Customer);
            ApplayPadding(invoiceSpecParams.PageSize * (invoiceSpecParams.PageIndex - 1), invoiceSpecParams.PageSize);
            AddOrderByDescending(x => x.Date);
        }
        
    }
}
