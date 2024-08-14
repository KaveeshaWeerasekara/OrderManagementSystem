using Microsoft.EntityFrameworkCore;
using OrderManagement.DTOs;
using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;
using OrderManagement.Models;

namespace OrderManagement.Service.ProductService
{
    public class OrderProductService : IOrderProductService
    {
        private readonly ApplicationDBContext context;
        public OrderProductService(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }

        public BaseResponse CreateOrderProduct(CreateOrderProductRequest request)
        {
            BaseResponse response;
            try
            {
                OrderProductModel newOrder = new OrderProductModel();
               // newOrder.OrderProductID = request.OrderProductID;
                newOrder.OrderID = request.OrderID;
                newOrder.ProductID = request.ProductID;
                newOrder.Quantity = request.Quantity;
                
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
                      //  orderProduct.OrderProductID = filteredOrderProduct.id;
                        orderProduct.OrderID = filteredOrderProduct.OrderID;
                        orderProduct.ProductID = filteredOrderProduct.ProductID;
                        orderProduct.Quantity = filteredOrderProduct.Quantity;
                     
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
                        OrderID = orderProduct.OrderID,
                        ProductID = orderProduct.ProductID,
                        Quantity = orderProduct.Quantity,
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

        
         public BaseResponse UpdateOrderProductsById(int id, UpdateOrderProductRequest request)
         {
             BaseResponse response;
             try
             {

                 using (context)
                 {
                     OrderProductModel filteredOrderProduct = context.OrderProducts.Where(orderProduct => orderProduct.OrderID == id).FirstOrDefault();
                     if (filteredOrderProduct != null)
                     {
                        filteredOrderProduct.Quantity = request.Quantity;

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

        public BaseResponse GetProductListByOrderID(int id) //get product list by giving order id
        {
            BaseResponse response;
            try
            {
                using (context)
                {
                    var orderProducts =  context.OrderProducts
                .Where(op => op.OrderID == id)
                .ToList();

                    if (!orderProducts.Any())
                    {
                        return new BaseResponse
                        {
                            status_code = StatusCodes.Status404NotFound,
                            data = new { message = "No products found for the given order ID" }
                        };
                    }
                    var productIds = orderProducts.Select(op => op.ProductID).Distinct().ToList();
                    var products =  context.Products

                         .Where(p => productIds.Contains(p.id))
                         .ToList();
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = products
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

        public BaseResponse GetProductListByUserID(int id) // get product list by giving user id
        {
            BaseResponse response;
            try
            {
                using (context)
                {
                    var orders = context.Orders
                .Where(op => op.UserID == id)
                .ToList();

                    if (!orders.Any())
                    {
                        return new BaseResponse
                        {
                            status_code = StatusCodes.Status404NotFound,
                            data = new { message = "No orders" }
                        };
                    }
                    var orderProductIds = context.OrderProducts
                .Where(op => orders.Select(o => o.id).Contains(op.OrderID))
                .Select(op => op.ProductID)
                .Distinct()
                .ToList();

                    if (!orderProductIds.Any())
                    {
                        return new BaseResponse
                        {
                            status_code = StatusCodes.Status404NotFound,
                            data = new { message = "No products found for these orders" }
                        };
                    }

                    var products = context.Products
             .Where(p => orderProductIds.Contains(p.id))
             .ToList();
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = products
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

    }
}
