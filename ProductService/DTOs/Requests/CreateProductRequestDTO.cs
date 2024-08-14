using System.ComponentModel.DataAnnotations;

namespace ProductsService.DTOs.Requests
{
    public class CreateProductRequestDTO
    {

        //[Required]
       // public int productID { get; set; }
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
