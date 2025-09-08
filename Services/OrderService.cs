using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Northwind_API.Data;
using Northwind_API.Models.Models;
using Northwind_API.Services.Interfaces;

namespace Northwind_API.Services;

public class OrderService : IOrderService
{
    private readonly AppDBContext _context;

    public OrderService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        var orders = await _context.orders.ToListAsync();
        return orders;
    }

    public async Task<Order?> GetOrderById(short Id)
    {
        var order = await _context.orders.FindAsync(Id);

        if (order == null)
            return null;

        return order;
    }

    public async Task<Order> CreateNewOrder(Order order)
    {
        _context.orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order> UpdateOrder(Order order)
    {
        _context.orders.Update(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<bool> DeleteOrderById(short Id)
    {
        var removeOrder = await _context.orders.FindAsync(Id);
        if (removeOrder == null)
        {
            return false;
        }

        _context.orders.Remove(removeOrder);
        await _context.SaveChangesAsync();
        return true;

    }


}
