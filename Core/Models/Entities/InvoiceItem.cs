using System.Reflection.Metadata;

namespace Core.Models.Entities
{
    public class InvoiceItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public int InvoiceId { get; set; } 
        public Invoice Invoice { get; set; }
    }
}
