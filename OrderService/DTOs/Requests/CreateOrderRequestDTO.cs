using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs.Requests
{
    public class CreateOrderRequestDTO
    {
        [Required]
        public long userID { get; set; }
        [Required]
        public DateTime orderPlaceDate { get; set; }
        [Required]
        //  public int Quantity { get; set; }

        public List<OrderProductRequest> ProductsOrder { get; set; } = new List<OrderProductRequest>();
    }
    public class OrderProductRequest
    {
        public long productID { get; set; }
        public int quantity { get; set; }
    }
}
