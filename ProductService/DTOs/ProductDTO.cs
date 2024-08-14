using System.ComponentModel.DataAnnotations;

namespace ProductsService.DTOs
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
