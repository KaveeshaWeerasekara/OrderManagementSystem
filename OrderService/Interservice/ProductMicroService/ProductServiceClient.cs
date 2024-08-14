using System.Net.Http;
using System.Text;
using System.Text.Json;
using OrderService.Interservice.ProductMicroService.DTOs;
using OrderService.Interservice.ProductMicroService.DTOs.GetProductResponseDTOs;
using OrderService.Interservice.ProductMicroService.DTOs.UpdateProductByIdResponseDTOs;

namespace OrderService.Interservice.ProductMicroService
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient httpClient;
        private readonly string _productServiceBaseUrl;

        public ProductServiceClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            _productServiceBaseUrl = configuration["ProductService:BaseUrl"] ?? throw new ArgumentNullException("Product service base URL is required");
        }
        public async Task<ProductDTO?> GetProductById(long productId)
        {
            ProductDTO? productDTO = null;
            var response = await httpClient.GetAsync($"{_productServiceBaseUrl}/api/Product/getProductById/{productId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                GetProductByIdResponseDTO? responseDTO = JsonSerializer.Deserialize<GetProductByIdResponseDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                productDTO = responseDTO?.status_code == StatusCodes.Status200OK ? responseDTO.data?.product : null;

            }

            return productDTO;
        }
        public async Task<string?> UpdateProductById(long productID,int storedQuantity)
        {
            var updateRequest = new 
            {
               
               Id=productID,
               StoredQuantity = storedQuantity
            };
            

            var productJson= JsonSerializer.Serialize(updateRequest);
            var content = new StringContent(productJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{_productServiceBaseUrl}/api/Product/updateProduct/{productID},{storedQuantity}",content);

            string? message = null;
            //checking response code and return message to orderService.cs
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                var responseDTO = JsonSerializer.Deserialize<UpdateProductByIdResponseDataDTO>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (responseDTO != null && responseDTO.status_code == 200)
                {
                    message = responseDTO.data?.message;
                }
            }

            return message;
        }
 

    }
}
