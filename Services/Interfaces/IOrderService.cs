using System;
using System.Data;
using Northwind_API.Models.Entities;

namespace Northwind_API.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetAllOrders();
    Task<Order?> GetOrderById(short Id);
    Task<bool> DeleteOrderById(short Id);
    Task<Order> CreateNewOrder(Order order);
    Task<Order> UpdateOrder(Order order);
}
