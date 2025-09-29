using System;
using System.Data;
using Northwind_API.Models.Models;

namespace Northwind_API.Services.Interfaces;

public interface IOrder
{
    Task<List<Order>> GetAllOrders();
    Task<Order?> GetOrderById(short Id);
    Task<bool> DeleteOrderById(short Id);
    Task<Order> CreateNewOrder(Order order);
    Task<Order> UpdateOrder(Order order);
}
