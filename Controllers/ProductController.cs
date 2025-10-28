using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Northwind_API.Helpers;
using Northwind_API.Models.Api;
using Northwind_API.Models.Models;
using Northwind_API.Services.Interfaces;
using Serilog;

namespace Northwind_API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProduct _productService;

        public ProductController(IProduct productService)
        {
            _productService = productService;
        }

        [HttpGet("all"), Authorize]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], GetAllProducts, Request recieved.", correlationId);

            try
            {
                var allProducts = await _productService.GetAllProducts();

                if (allProducts == null)
                {
                    Log.Warning(
                        "[{correlationId}], GetAllProducts, No Products could be found.",
                        correlationId
                    );
                    return new NotFoundObjectResult(
                        ApiResponse<string>.ErrorResult(
                            correlationId,
                            "No Products could be found.",
                            string.Empty
                        )
                    );
                }

                Log.Information(
                    "[{correlationId}], GetAllProducts, All Products returned.",
                    correlationId
                );
                return new OkObjectResult(
                    ApiResponse<List<Product>>.Result(
                        correlationId,
                        "All products returned.",
                        allProducts
                    )
                );
            }
            catch (System.Exception ex)
            {
                Log.Error(
                    "[{correlationId}], GetAllProducts, An Error occurred: {error}.",
                    correlationId,
                    ex.Message
                );
                return new BadRequestObjectResult(
                    ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message)
                );
            }
        }

        [HttpGet("{Id}"), Authorize]
        public async Task<ActionResult<Product>> GetProductById(short Id)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], GetProductsById, Request recieved.", correlationId);

            try
            {
                var productById = await _productService.GetProductById(Id);

                if (productById == null)
                {
                    Log.Information(
                        "[{correlationId}], GetProductById, Product with Id {id} was not found.",
                        correlationId,
                        Id
                    );
                    return new NotFoundObjectResult(
                        ApiResponse<string>.ErrorResult(
                            correlationId,
                            $"Product with Id {Id} was not found.",
                            string.Empty
                        )
                    );
                }

                Log.Information(
                    "[{correlationId}], GetProductById, Product with Id {id} was found.",
                    correlationId,
                    Id
                );
                return new OkObjectResult(
                    ApiResponse<Product>.Result(
                        correlationId,
                        $"Product with Id {Id} found.",
                        productById
                    )
                );
            }
            catch (System.Exception ex)
            {
                Log.Error(
                    "[{correlationId}], GetOrdedrById, An Error occurred: {error}.",
                    correlationId,
                    ex.Message
                );
                return new BadRequestObjectResult(
                    ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message)
                );
            }
        }

        [HttpPost("add"), Authorize]
        public async Task<ActionResult<Product>> CreateProduct(Product newProduct)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], CreateProduct, Request recieved.", correlationId);

            try
            {
                if (!ModelState.IsValid)
                {
                    Log.Fatal(
                        "[{correlationId}], CreateProduct, Invalid Model state: {error}.",
                        correlationId,
                        ModelState
                    );
                    return new BadRequestObjectResult(
                        ApiResponse<ModelStateDictionary>.ErrorResult(
                            correlationId,
                            $"An error occurred.",
                            ModelState
                        )
                    );
                }

                if (newProduct == null)
                {
                    Log.Error(
                        "[{correlationId}], CreateProduct, Product can not be null.",
                        correlationId
                    );
                    return new BadRequestObjectResult(
                        ApiResponse<string>.ErrorResult(
                            correlationId,
                            $"Product can not be null.",
                            string.Empty
                        )
                    );
                }

                var createdProduct = await _productService.CreateNewProduct(newProduct);

                Log.Information(
                    "[{correlationId}], CreateProduct, Product with Id {id} created successfully.",
                    correlationId,
                    createdProduct.ProductId
                );
                return CreatedAtAction(
                    nameof(GetProductById),
                    new { Id = createdProduct.ProductId },
                    ApiResponse<Product>.Result(
                        correlationId,
                        $"Product with Id {createdProduct.ProductId} created successfully.",
                        createdProduct
                    )
                );
            }
            catch (System.Exception ex)
            {
                Log.Error(
                    "[{correlationId}], CreateProduct, An Error occurred: {error}.",
                    correlationId,
                    ex.Message
                );
                return new BadRequestObjectResult(
                    ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message)
                );
            }
        }

        [HttpDelete("{Id}"), Authorize]
        public async Task<ActionResult<string>> DeleteProductById(short Id)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], DeleteProductById, Request recieved.", correlationId);

            try
            {
                var wasDeleted = await _productService.DeleteProductById(Id);

                if (!wasDeleted)
                {
                    Log.Information(
                        "[{correlationId}], DeleteProductById, Product with Id {id} was not found.",
                        correlationId,
                        Id
                    );
                    return new NotFoundObjectResult(
                        ApiResponse<string>.ErrorResult(
                            correlationId,
                            $"Product with Id {Id} was not found.",
                            string.Empty
                        )
                    );
                }

                Log.Information(
                    "[{correlationId}], DeleteProductById, Product with Id {id} deleted successfully.",
                    correlationId,
                    Id
                );
                return new ObjectResult(
                    ApiResponse<string>.Result(
                        correlationId,
                        $"Product with Id {Id} was deleted.",
                        string.Empty
                    )
                )
                {
                    StatusCode = StatusCodes.Status204NoContent,
                };
            }
            catch (System.Exception ex)
            {
                Log.Error(
                    "[{correlationId}], DeletOrerById, An Error occurred: {error}.",
                    correlationId,
                    ex.Message
                );
                return new BadRequestObjectResult(
                    ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message)
                );
            }
        }

        [HttpPut("{Id}"), Authorize]
        public async Task<ActionResult<Product>> UpdateProduct(short Id, Product product)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], UpdateProduct, Request recieved.", correlationId);

            try
            {
                if (product == null)
                {
                    Log.Error(
                        "[{correlationId}], UpdateProduct, Product can not be null.",
                        correlationId
                    );
                    return new BadRequestObjectResult(
                        ApiResponse<string>.ErrorResult(
                            correlationId,
                            $"Product can not be null.",
                            string.Empty
                        )
                    );
                }

                if (Id != product.ProductId)
                {
                    Log.Error("[{correlationId}], UpdateProduct, ID mismatch.", correlationId);
                    return new BadRequestObjectResult(
                        ApiResponse<string>.ErrorResult(
                            correlationId,
                            $"ID mismatch.",
                            string.Empty
                        )
                    );
                }

                var updatedProduct = await _productService.UpdateProduct(product);
                if (product == null)
                {
                    Log.Information(
                        "[{correlationId}], UpdateProduct, Product with Id {id} was not found.",
                        correlationId,
                        Id
                    );
                    return new NotFoundObjectResult(
                        ApiResponse<string>.ErrorResult(
                            correlationId,
                            $"Product with Id {Id} was not found.",
                            string.Empty
                        )
                    );
                }

                Log.Information(
                    "[{correlationId}], UpdateProduct, Product with Id {id} updated successfully.",
                    correlationId,
                    updatedProduct.ProductId
                );
                return new OkObjectResult(
                    ApiResponse<Product>.Result(
                        correlationId,
                        $"Product with Id {updatedProduct.ProductId} updated successfully.",
                        updatedProduct
                    )
                );
            }
            catch (System.Exception ex)
            {
                Log.Error(
                    "[{correlationId}], UpdateProduct, An Error occurred: {error}.",
                    correlationId,
                    ex.Message
                );
                return new BadRequestObjectResult(
                    ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message)
                );
            }
        }
    }
}
