using OrderService.Interservice.ProductMicroService.DTOs;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Interservice.OrderProductService.OrderProductDTOs
{
    public class OrderProductListData
    {
        public List<OrderProductDTO> orderProducts { get; set; }
    }
    public class OrderProductDTO
    {
        [Required]
        public long id { get; set; }
        // [Required] 
        // public int OrderProductID { get;set; }
        [Required]
        public long orderID { get; set; }
        [Required]
        public long productID { get; set; }
        [Required]
        public int quantity { get; set; }
    }
    public class OrderProductListed    //wrapper
    {
        public int status_code { get; set; }
        public OrderProductListData productListData { get; set; }
    }
}
