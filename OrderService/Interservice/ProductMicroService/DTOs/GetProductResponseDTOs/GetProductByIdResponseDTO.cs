namespace OrderService.Interservice.ProductMicroService.DTOs.GetProductResponseDTOs
{
    public class GetProductByIdResponseDTO
    {
        public int status_code {  get; set; }
        public GetProductByIdResponseDataDTO? data { get; set; }
    }


    public class GetProductByIdResponseDataDTO
    {
        public ProductDTO? product { get; set; }
        public string? message { get; set; } 
    }
}
