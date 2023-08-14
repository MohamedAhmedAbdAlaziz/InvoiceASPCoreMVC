using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Dtos.ViewModels
{
    public class ITemViewModel
    {

        [Required]
        public string Name { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]

        public int Quantity { get; set; }
    }
}
