using System.ComponentModel.DataAnnotations;

namespace OrderManagement.DTOs.Requests
{
    public class CreateOrderRequest
    {
      //  [Required]
       // public int OrderID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public DateTime OrderPlaceDate { get; set; }
        [Required]
      //  public int Quantity { get; set; }

        public List<OrderProductRequest> ProductsOrder { get; set; }
    }
    public class OrderProductRequest
    {
        public long ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
