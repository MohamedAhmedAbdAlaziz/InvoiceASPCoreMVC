 

namespace Core.Models.Entities
{
    public class Invoice: BaseEntity
    {
      
        public DateOnly Date { get; set; }
        public int Code { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public ICollection<Item> Items { get; set; }


    }
}
