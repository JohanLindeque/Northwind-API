using System;
using Northwind_API.Data;
using Northwind_API.Models.Models;
using Northwind_API.Services.Interfaces;

namespace Northwind_API.Services;

public class ProductService : IProduct
{
    private readonly AppDBContext _context;

    public ProductService(AppDBContext context)
    {
        _context = context;
    }
    
    public Task<Product> CreateNewProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductById(short Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetProductById(short Id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}
