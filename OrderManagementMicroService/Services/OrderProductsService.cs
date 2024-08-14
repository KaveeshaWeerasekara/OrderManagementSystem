using OrderProductService.DTOs;
using OrderProductService.DTOs.Requests;
using OrderProductService.DTOs.Responses;
using OrderProductService.Models;

namespace OrderProductService.Services
{
    public class OrderProductsService:IOrderProductService
    {
        private readonly ApplicationDBContext context;
        public OrderProductsService(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }
        public BaseResponse CreateOrderProduct(CreateOrderProductRequestDTO request)
        {
            BaseResponse response;
            try
            {
                OrderProductModel newOrder = new OrderProductModel();
                newOrder.OrderID = request.orderID;
                newOrder.ProductID = request.productID;
                newOrder.Quantity = request.quantity;

                using (context)
                {
                    context.Add(newOrder);
                    context.SaveChanges();

                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new order product" }
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
        public BaseResponse DeleteOrderProductsById(int id)
        {
            BaseResponse response;
            try
            {

                using (context)
                {
                    OrderProductModel? orderToDelete = context.OrderProducts.Where(order => order.OrderID == id).FirstOrDefault();
                    if (orderToDelete != null)
                    {
                        context.OrderProducts.Remove(orderToDelete);
                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Ordered Products deleted successfully" }
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
        public BaseResponse GetOrderProductsById(int id)
        {
            BaseResponse response;
            try
            {
                OrderProductDTO orderProduct = new OrderProductDTO();
                using (context)
                {
                    OrderProductModel? filteredOrderProduct = context.OrderProducts.Where(orderProduct => orderProduct.id == id).FirstOrDefault();
                    if (filteredOrderProduct != null)
                    {
                        orderProduct.orderID = filteredOrderProduct.OrderID;
                        orderProduct.productID = filteredOrderProduct.ProductID;
                        orderProduct.quantity = filteredOrderProduct.Quantity;

                    }
                    else
                    {
                        orderProduct = null;
                    }
                }
                if (orderProduct != null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { orderProduct }
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
        public BaseResponse OrderProductList()
        {
            BaseResponse response;
            try
            {
                List<OrderProductDTO> orderProducts = new List<OrderProductDTO>();
                using (context)
                {
                    context.OrderProducts.ToList().ForEach(orderProduct => orderProducts.Add(new OrderProductDTO
                    {
                        id = orderProduct.id,
                        orderID = orderProduct.OrderID,
                        productID = orderProduct.ProductID,
                        quantity = orderProduct.Quantity,
                    }));

                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { orderProducts }
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
        public BaseResponse UpdateOrderProductsById(int id, UpdateOrderProductRequestDTO request)
        {
            BaseResponse response;
            try
            {

                using (context)
                {
                    OrderProductModel? filteredOrderProduct = context.OrderProducts.Where(orderProduct => orderProduct.OrderID == id).FirstOrDefault();
                    if (filteredOrderProduct != null)
                    {
                        filteredOrderProduct.Quantity = request.quantity;

                        context.SaveChanges();
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Order Product updated successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No ordered product found" }
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
    }
}
