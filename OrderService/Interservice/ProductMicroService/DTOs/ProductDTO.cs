using System.ComponentModel.DataAnnotations;

namespace OrderService.Interservice.ProductMicroService.DTOs
{
    
    public class ProductDTO
    {
        [Required]
        public long id { get; set; }

        [Required]
        public string productName { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public int storedQuantity { get; set; }
    }
  
}
