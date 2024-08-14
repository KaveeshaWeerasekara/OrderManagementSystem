using ProductsService.DTOs;
using ProductsService.DTOs.Requests;
using ProductsService.DTOs.Responses;
using ProductsService.Models;

namespace ProductsService.Services
{
    public class ProductsServices:IProductService
    {
        private readonly ApplicationDBContext context;
        public ProductsServices(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }
        public BaseResponse CreateProduct(CreateProductRequestDTO request)
        {
            BaseResponse response;
            try
            {
                ProductModel newProduct = new ProductModel();
                //  newProduct.ProductID = request.ProductID;
                newProduct.ProductName = request.productName;
                newProduct.Price = request.price;
                newProduct.Category = request.category;
                newProduct.StoredQuantity = request.storedQuantity;

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
                        product.productName = filteredProduct.ProductName;
                        product.price = filteredProduct.Price;
                        product.category = filteredProduct.Category;
                        product.storedQuantity = filteredProduct.StoredQuantity;

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
                        productName = product.ProductName,
                        price = product.Price,
                        category = product.Category,
                        storedQuantity = product.StoredQuantity,

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
        public BaseResponse UpdateProductById(int id, UpdateProductRequestDTO request)
        {
            BaseResponse response;
            try
            {

                using (context)
                {
                    ProductModel? filteredProduct = context.Products.Where(product => product.id == id).FirstOrDefault();
                    if (filteredProduct != null)
                    {
                        filteredProduct.ProductName = request.productName;
                        filteredProduct.Price = request.price;
                        filteredProduct.Category = request.category;
                        filteredProduct.StoredQuantity = request.storedQuantity;

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

        public BaseResponse UpdateProduct(long productId, int storedQuantity)
        {
            BaseResponse response;
            try
            {

                using (context)
                {
                    ProductModel? filteredProduct = context.Products.Where(product => product.id == productId).FirstOrDefault();
                    if (filteredProduct != null)
                    {
                        //filteredProduct.ProductName = request.productName;
                       // filteredProduct.Price = request.price;
                       // filteredProduct.Category = request.category;
                        filteredProduct.StoredQuantity = storedQuantity;

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
