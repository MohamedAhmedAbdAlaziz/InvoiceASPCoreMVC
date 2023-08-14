 
namespace EcommerceProject.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int Code { get; set; }
        public decimal Amount { get; set; }
       
        public string Customer { get; set; }

    
    }
}
