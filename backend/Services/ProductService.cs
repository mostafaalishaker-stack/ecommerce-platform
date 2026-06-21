using Microsoft.EntityFrameworkCore;
using ECommerce.Data;
using ECommerce.DTOs;
using ECommerce.Models;

namespace ECommerce.Services;

public class ProductService
{
    private readonly AppDbContext _context;
    public ProductService(AppDbContext context) => _context = context;

    public async Task<List<ProductResponse>> GetAll(string? category, string? search)
    {
        var query = _context.Products.AsQueryable();
        if (!string.IsNullOrEmpty(category)) query = query.Where(p => p.Category == category);
        if (!string.IsNullOrEmpty(search)) query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
        return await query.Select(p => new ProductResponse(p.Id, p.Name, p.Description, p.Price, p.ImageUrl, p.Category, p.Stock)).ToListAsync();
    }

    public async Task<ProductResponse?> GetById(int id)
    {
        var p = await _context.Products.FindAsync(id);
        return p is null ? null : new ProductResponse(p.Id, p.Name, p.Description, p.Price, p.ImageUrl, p.Category, p.Stock);
    }

    public async Task<ProductResponse> Create(CreateProductRequest req)
    {
        var p = new Product { Name = req.Name, Description = req.Description, Price = req.Price, ImageUrl = req.ImageUrl, Category = req.Category, Stock = req.Stock };
        _context.Products.Add(p);
        await _context.SaveChangesAsync();
        return new ProductResponse(p.Id, p.Name, p.Description, p.Price, p.ImageUrl, p.Category, p.Stock);
    }

    public async Task<bool> Delete(int id)
    {
        var p = await _context.Products.FindAsync(id);
        if (p is null) return false;
        _context.Products.Remove(p);
        await _context.SaveChangesAsync();
        return true;
    }
}
