using OrderService.Interservice.ProductMicroService.DTOs.GetProductResponseDTOs;

namespace OrderService.Interservice.ProductMicroService.DTOs.UpdateProductByIdResponseDTOs
{
    public class UpdateProductByIdResponseDataDTO
    {
        public int status_code { get; set; }
        public UpdateProductByIdResponseDataDTOs? data { get; set; }
    }
    public class UpdateProductByIdResponseDataDTOs
    {
       /* public ProductDTO? product { get; set; }*/
        public string? message { get; set; }
    }
}
