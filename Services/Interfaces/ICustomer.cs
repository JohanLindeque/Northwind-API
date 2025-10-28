using System;
using Northwind_API.Models.Models;

namespace Northwind_API.Services.Interfaces;

public interface ICustomer
{
    Task<List<Customer>> GetAllCustomers();
    Task<Customer?> GetCustomerById(short Id);
    Task<bool> DeleteCustomerById(short Id);
    Task<Customer> CreateNewCustomer(Customer customer);
    Task<Customer> UpdateCustomer(Customer customer);
}
