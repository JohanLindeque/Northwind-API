using System;
using Northwind_API.Services.Interfaces;

namespace Northwind_API.Services;

public class CustomerService : ICustomer
{

 public Task<Customer> CreateNewCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCustomerById(short Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Customer>> GetAllCustomers()
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetCustomerById(short Id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }
}
