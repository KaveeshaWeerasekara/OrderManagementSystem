using System.ComponentModel.DataAnnotations;

namespace OrderProductService.DTOs
{
    public class OrderProductDTO
    {
        [Required]
        public long id { get; set; }
        
        [Required]
        public long orderID { get; set; }
        [Required]
        public long productID { get; set; }
        [Required]
        public int quantity { get; set; }
    }
}
