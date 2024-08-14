using OrderService.Interservice.OrderProductService.OrderProductDTOs;
using System.Net.Http;
using System.Text.Json;

namespace OrderService.Interservice.OrderProductService
{
    public class OrderProductServiceClient:IOrderProductServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _orderProductServiceBaseUrl;

        public OrderProductServiceClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _orderProductServiceBaseUrl = configuration["OrderProductService:BaseUrl"];
        }

       /* public async Task<bool> IsValidUserAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_orderProductServiceBaseUrl}/api/orderProduct/list-products");

            if (!response.IsSuccessStatusCode)
            {
                // If the request fails, return false (user is not valid)
                return false;
            }
            var content = await response.Content.ReadAsStringAsync();
            var orderProductList = JsonConvert.DeserializeObject<List<OrderProductDTO>>(content);
            var isValidUser = orderProductList.Any(op => op.UserID == userId);
            return isValidUser;
        }*/
        public async Task<OrderProductDTO> GetOrderProductByIdAsync()
        {
            var response = await _httpClient.GetAsync($"{_orderProductServiceBaseUrl}/api/orderProduct/list-products");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<OrderProductDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }
    }
}
