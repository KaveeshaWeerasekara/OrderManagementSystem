using System.ComponentModel.DataAnnotations;

namespace OrderProductService.DTOs.Requests
{
    public class CreateOrderProductRequestDTO
    {
       // [Required]
      //  public int orderProductID { get; set; }
        [Required]
        public int orderID { get; set; }
        [Required]
        public int productID { get; set; }
        [Required]
        public int quantity { get; set; }
    }
}
