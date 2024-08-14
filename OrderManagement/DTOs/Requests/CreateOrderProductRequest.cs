using System.ComponentModel.DataAnnotations;

namespace OrderManagement.DTOs.Requests
{
    public class CreateOrderProductRequest
    {
        [Required]
        public int OrderProductID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        
        
    }
}
