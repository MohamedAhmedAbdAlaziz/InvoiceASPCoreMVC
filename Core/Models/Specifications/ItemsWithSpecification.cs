using Core.Models.Entities;

namespace Core.Models.Specifications
{
    public class ItemsWithSpecification : BaseSpecification<Item>
    {
        public ItemsWithSpecification(int InvoiceID) : base(x => x.InvoiceId== InvoiceID)
        { 
        }


    }
    }