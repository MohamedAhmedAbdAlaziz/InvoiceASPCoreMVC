using System.Reflection.Metadata;

namespace Core.Models.Entities
{
    public class Item: BaseEntity
    {
        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity  { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }




    }
}
