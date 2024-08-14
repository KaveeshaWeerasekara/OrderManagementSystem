using System.ComponentModel.DataAnnotations;

namespace OrderManagement.DTOs
{
    public class ProductDTO
    {
        [Required]
        public long id { get; set; }
       // [Required]
       // public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int StoredQuantity { get; set; }

    }
}
