using Microsoft.EntityFrameworkCore;
using ECommerce.Data;
using ECommerce.DTOs;
using ECommerce.Models;

namespace ECommerce.Services;

public class OrderService
{
    private readonly AppDbContext _context;
    public OrderService(AppDbContext context) => _context = context;

    public async Task<List<OrderResponse>> GetUserOrders(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .Select(o => new OrderResponse(o.Id, o.CreatedAt, o.Total, o.Status,
                o.Items.Select(i => new OrderItemResponse(i.ProductName, i.Price, i.Quantity)).ToList()))
            .ToListAsync();
    }

    public async Task<OrderResponse> CreateOrder(string userId, CreateOrderRequest req)
    {
        var order = new Order { UserId = userId, ShippingAddress = req.ShippingAddress, Status = "Pending" };

        foreach (var item in req.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product is null)
                throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");

            if (product.Stock < item.Quantity)
                throw new InvalidOperationException($"Insufficient stock for product '{product.Name}'. Available: {product.Stock}, requested: {item.Quantity}.");

            order.Items.Add(new OrderItem { ProductId = product.Id, ProductName = product.Name, Price = product.Price, Quantity = item.Quantity });
            order.Total += product.Price * item.Quantity;
            product.Stock -= item.Quantity;
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return new OrderResponse(order.Id, order.CreatedAt, order.Total, order.Status,
            order.Items.Select(i => new OrderItemResponse(i.ProductName, i.Price, i.Quantity)).ToList());
    }

    public async Task<OrderResponse?> UpdateStatus(int orderId, string status)
    {
        var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderId);
        if (order is null) return null;
        order.Status = status;
        await _context.SaveChangesAsync();
        return new OrderResponse(order.Id, order.CreatedAt, order.Total, order.Status,
            order.Items.Select(i => new OrderItemResponse(i.ProductName, i.Price, i.Quantity)).ToList());
    }

    public async Task<List<OrderResponse>> GetAllOrders()
    {
        return await _context.Orders.Include(o => o.Items)
            .Select(o => new OrderResponse(o.Id, o.CreatedAt, o.Total, o.Status,
                o.Items.Select(i => new OrderItemResponse(i.ProductName, i.Price, i.Quantity)).ToList()))
            .ToListAsync();
    }
}
