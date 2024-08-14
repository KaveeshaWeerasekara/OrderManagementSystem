using OrderService.DTOs;
using OrderService.DTOs.Requests;
using OrderService.DTOs.Responses;
using OrderService.Interservice.OrderProductService;
using OrderService.Interservice.OrderProductService.OrderProductDTOs;
//using OrderService.Interservice.OrderService;
using OrderService.Interservice.ProductMicroService;
using OrderService.Interservice.ProductMicroService.DTOs;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrdersService : IOrderService
    {
        private readonly ApplicationDBContext dbcontext;
        private readonly IProductServiceClient _productServiceClient;
        private readonly IOrderProductServiceClient _orderProductServiceClient;
      //  private readonly IOrderServiceClient _orderServiceClient;

        public OrdersService(ApplicationDBContext context, IProductServiceClient productServiceClient, IOrderProductServiceClient orderProductServiceClient)
        {
            dbcontext = context;
            _productServiceClient = productServiceClient;
            _orderProductServiceClient = orderProductServiceClient;
          //  _orderServiceClient = orderServiceClient;

        }
        public async Task<BaseResponse> CreateOrder(CreateOrderRequestDTO request) // create an order by checking quantity and product
        {
            BaseResponse response;
            try
            {
                List<ProductDTO> orderProducts = new List<ProductDTO>();
                List<OrderProductDTO> orderedProducts = new List<OrderProductDTO>();

                foreach (var productsOrder in request.ProductsOrder)
                {
                    // Get product details
                    ProductDTO? currentProduct = await _productServiceClient.GetProductById(productsOrder.productID);

                    if (currentProduct == null)
                    {
                        return new BaseResponse
                        {
                            status_code = StatusCodes.Status404NotFound,
                            data = new { message = $"Product with ID {productsOrder.productID} not found." }
                        };
                    }


                    if (currentProduct.storedQuantity < productsOrder.quantity)
                    {
                        return new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = $"Insufficient stock for product ID {productsOrder.productID}." }
                        };
                    }

                    currentProduct.storedQuantity -= productsOrder.quantity;

                    try
                    {
                        OrderModel newOrder = new OrderModel
                        {
                            UserID = request.userID,
                            OrderPlaceDate = request.orderPlaceDate,
                            Quantity = productsOrder.quantity,

                        };
                       

                        using (dbcontext)
                        {
                            dbcontext.Add(newOrder);
                            dbcontext.SaveChanges();

                        }
                       
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Successfully created the new order" }
                        };

                        orderProducts.Add(new ProductDTO
                        {
                            id = currentProduct.id,
                            storedQuantity = currentProduct.storedQuantity
                        });
                        var updatedProduct = await _productServiceClient.UpdateProductById(currentProduct.id, currentProduct.storedQuantity);

                        if (updatedProduct == null)
                        {
                            return new BaseResponse
                            {
                                status_code = StatusCodes.Status500InternalServerError,
                                data = new { message = $"Failed to update product ID {productsOrder.productID}." }
                            };
                        }
                        return response;
                    }
                    catch (Exception ex) {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status500InternalServerError,
                            data = new { message = "Internal Server Error : " + ex.Message }
                        };
                        return response;
                    }
 
                }
                return new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Order successfully created." }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = $"Internal Server Error: {ex.Message}" }
                };
            }
            

        }

        public BaseResponse DeleteOrderById(int id)
        {
            BaseResponse response;
            try
            {

                using (dbcontext)
                {
                    OrderModel? orderToDelete = dbcontext.Orders.Where(order => order.id == id).FirstOrDefault();
                    if (orderToDelete != null)
                    {
                        dbcontext.Orders.Remove(orderToDelete);
                        dbcontext.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Order deleted successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No order found" }
                        };
                    }
                }

                return response;

            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };
                return response;
            }
        }
        public BaseResponse GetOrderById(int id)
        {
            BaseResponse response;
            try
            {
                OrderDTO? order = new OrderDTO();
                using (dbcontext)
                {
                    OrderModel? filteredOrder = dbcontext.Orders.Where(order => order.id == id).FirstOrDefault();
                    if (filteredOrder != null)
                    {
                        // order.OrderID = filteredOrder.OrderID;
                        order.userID = filteredOrder.UserID;
                        order.orderPlaceDate = filteredOrder.OrderPlaceDate;

                    }
                    else
                    {
                        order = null;
                    }
                }
                if (order != null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { order }
                    };
                }
                else
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "No order found" }
                    };
                }
                return response;

            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };
                return response;
            }
        }

        public DTOs.Responses.BaseResponse OrderList()
        {
            BaseResponse response;
            try
            {
                List<OrderDTO> orders = new List<OrderDTO>();
                using (dbcontext)
                {
                    dbcontext.Orders.ToList().ForEach(order => orders.Add(new OrderDTO
                    {
                        id = order.id,
                        userID = order.UserID,
                        orderPlaceDate = order.OrderPlaceDate,
                    }));

                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { orders }
                };
                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };
                return response;
            }
        }

      
    }
}
