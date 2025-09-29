using System;
using Northwind_API.Models.Models;

namespace Northwind_API.Services.Interfaces;

public interface IProduct
{
    Task<List<Product>> GetAllProducts();
    Task<Product?> GetProductById(short Id);
    Task<bool> DeleteProductById(short Id);
    Task<Product> CreateNewProduct(Product product);
    Task<Product> UpdateProduct(Product product);

}
