using Microsoft.AspNetCore.Mvc;
using OrderManagement.DTOs;
using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;
using OrderManagement.Models;

namespace OrderManagement.Service.ProductService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDBContext context;
        public OrderService(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }

        public BaseResponse CreateOrder(CreateOrderRequest request) // create an order by checking quantity and product
        {
            BaseResponse response;
            try
            {
                List<OrderProductModel> orderProducts = new List<OrderProductModel>();
                List<ProductModel> productUpdates = new List<ProductModel>();

                foreach (OrderProductRequest productsOrder in request.ProductsOrder)
                {
                    var productOrder = context.Products.FirstOrDefault(p => p.id == productsOrder.ProductID);
                    if (productOrder == null)
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status404NotFound,
                            data = new { message = $"Product with ID {productsOrder.ProductID} not found" }
                        };
                        return response;
                    }
                    if (productOrder.StoredQuantity < productsOrder.Quantity)
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = $"Insufficient stock quantity for product ID {productsOrder.ProductID}" }
                        };
                        return response;
                    }
                    productOrder.StoredQuantity -= productsOrder.Quantity;
                    orderProducts.Add(new OrderProductModel
                    {
                        ProductID = productsOrder.ProductID,
                        Quantity = productsOrder.Quantity
                    });
                    productUpdates.Add(productOrder);
                }

                var newOrder = new OrderModel
                {
                    UserID = request.UserID,
                    OrderPlaceDate = request.OrderPlaceDate,
                   
                };
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Orders.Add(newOrder);
                        foreach (var productOrder in productUpdates)
                        {
                            context.Products.Update(productOrder);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new order" }
                };
                return response;
            }

              /*  ProductModel? product = context.Products.FirstOrDefault(p => p.id == ProductID);
                if (product == null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status404NotFound,
                        data = new { message = "Product not found" }
                    };
                    return response;
                }
                if (product.StoredQuantity < request.Quantity)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "Insufficient stock quantity" }
                    };
                    return response;
                }
                OrderModel newOrder = new OrderModel();
               // newOrder.OrderID = request.OrderID;
                newOrder.UserID = request.UserID;
                newOrder.OrderPlaceDate = request.OrderPlaceDate;
                newOrder.Quantity = request.Quantity;
                
                using (context)
                {
                    context.Add(newOrder);
                    context.SaveChanges();

                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new order" }
                };
                return response;
            }*/
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
      /*  public BaseResponse CreateOrderByUserID([FromBody] CreateOrderRequest request, [FromQuery] int UserID) // create an order by giving user ID
        {
            BaseResponse response;
            try
            {
                UserModel? user = context.Users.FirstOrDefault(u => u.id == UserID);
                if (user == null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status404NotFound,
                        data = new { message = "No user found" }
                    };
                    return response;
                }
               
                OrderModel newOrder = new OrderModel();
                // newOrder.OrderID = request.OrderID;
                newOrder.UserID = request.UserID;
                newOrder.OrderPlaceDate = request.OrderPlaceDate;
                newOrder.Quantity = request.Quantity;

                using (context)
                {
                    context.Add(newOrder);
                    context.SaveChanges();

                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new order" }
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
        }*/

        public BaseResponse DeleteOrderById(int id)
        {
            BaseResponse response;
            try
            {

                using (context)
                {
                    OrderModel? orderToDelete = context.Orders.Where(order => order.id == id).FirstOrDefault();
                    if (orderToDelete != null)
                    {
                        context.Orders.Remove(orderToDelete);
                        context.SaveChanges();

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
                using (context)
                {
                    OrderModel? filteredOrder = context.Orders.Where(order => order.id == id).FirstOrDefault();
                    if (filteredOrder != null)
                    {
                       // order.OrderID = filteredOrder.OrderID;
                        order.UserID = filteredOrder.UserID;
                        order.OrderPlaceDate = filteredOrder.OrderPlaceDate;
                     
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

       
        public BaseResponse OrderList()
        {
            BaseResponse response;
            try
            {
                List<OrderDTO> orders = new List<OrderDTO>();
                using (context)
                {
                    context.Orders.ToList().ForEach(order => orders.Add(new OrderDTO
                    {
                       id = order.id,
                        UserID = order.UserID,
                        OrderPlaceDate = order.OrderPlaceDate,
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

       
       /* public BaseResponse UpdateOrderById(int id, UpdateOrderRequest request)
        {
            BaseResponse response;
            try
            {

                using (context)
                {
                    OrderModel filteredOrder = context.Orders.Where(order => order.OrderID == id).FirstOrDefault();
                    if (filteredOrder != null)
                    {
                        filteredOrder.ProductName = request.ProductName;
                        filteredOrder.Price = request.Price;
                        filteredProduct.Category = request.Category;

                        context.SaveChanges();
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Product updated successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No product found" }
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
        }*/

        
    }
}
