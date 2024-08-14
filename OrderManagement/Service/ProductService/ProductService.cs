using OrderManagement.DTOs;
using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;
using OrderManagement.Models;

namespace OrderManagement.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDBContext context;
        public ProductService(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }
        public BaseResponse CreateProduct(CreateProductRequest request)
        {
            BaseResponse response;
            try
            {
                ProductModel newProduct = new ProductModel();
              //  newProduct.ProductID = request.ProductID;
                newProduct.ProductName = request.ProductName;
                newProduct.Price = request.Price;
                newProduct.Category = request.Category;
                newProduct.StoredQuantity = request.StoredQuantity;
              
                using (context)
                {
                    context.Add(newProduct);
                    context.SaveChanges();

                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new product" }
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

        public BaseResponse DeleteProductById(int id)
        {
            BaseResponse response;
            try
            {

                using (context)
                {
                    ProductModel? productToDelete = context.Products.Where(product => product.id == id).FirstOrDefault();
                    if (productToDelete != null)
                    {
                        context.Products.Remove(productToDelete);
                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Product deleted successfully" }
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
        }

        public BaseResponse GetProductById(int id)
        {
            BaseResponse response;
            try
            {
                ProductDTO product = new ProductDTO();
                using (context)
                {
                    ProductModel? filteredProduct = context.Products.Where(product => product.id == id).FirstOrDefault();
                    if (filteredProduct != null)
                    {
                        product.id = filteredProduct.id;
                        product.ProductName = filteredProduct.ProductName;
                        product.Price = filteredProduct.Price;
                        product.Category = filteredProduct.Category;
                        product.StoredQuantity = filteredProduct.StoredQuantity;
             
                    }
                    else
                    {
                        product = null;
                    }
                }
                if (product != null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { product }
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

        public BaseResponse ProductList()
        {
            BaseResponse response;
            try
            {
                List<ProductDTO> products = new List<ProductDTO>();
                using (context)
                {
                    context.Products.ToList().ForEach(product => products.Add(new ProductDTO
                    {
                        id = product.id,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Category = product.Category,
                        StoredQuantity = product.StoredQuantity,

                    }));

                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { products }
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

        public BaseResponse UpdateProductById(int id, UpdateProductRequest request)
        {
            BaseResponse response;
            try
            {

                using (context)
                {
                    ProductModel? filteredProduct = context.Products.Where(product => product.id == id).FirstOrDefault();
                    if (filteredProduct != null)
                    {
                        filteredProduct.ProductName = request.ProductName;
                        filteredProduct.Price = request.Price;
                        filteredProduct.Category = request.Category;
                        filteredProduct.StoredQuantity = request.StoredQuantity;

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

        }
    }
}
