using Microsoft.AspNetCore.Identity;
using ECommerce.Models;

namespace ECommerce.Data;

public static class DbSeeder
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        if (context.Products.Any()) return;

        context.Products.AddRange(
            new Product { Name = "Wireless Headphones", Description = "Premium noise-cancelling wireless headphones", Price = 299.99m, ImageUrl = "/images/headphones.jpg", Category = "Electronics", Stock = 50 },
            new Product { Name = "Mechanical Keyboard", Description = "RGB mechanical gaming keyboard", Price = 149.99m, ImageUrl = "/images/keyboard.jpg", Category = "Electronics", Stock = 30 },
            new Product { Name = "Running Shoes", Description = "Lightweight running shoes with cushioning", Price = 89.99m, ImageUrl = "/images/shoes.jpg", Category = "Sports", Stock = 100 },
            new Product { Name = "Coffee Maker", Description = "Programmable 12-cup coffee maker", Price = 79.99m, ImageUrl = "/images/coffee.jpg", Category = "Home", Stock = 25 },
            new Product { Name = "Smart Watch", Description = "Fitness tracker with heart rate monitor", Price = 199.99m, ImageUrl = "/images/watch.jpg", Category = "Electronics", Stock = 40 },
            new Product { Name = "Backpack", Description = "Water-resistant laptop backpack 15.6", Price = 59.99m, ImageUrl = "/images/backpack.jpg", Category = "Accessories", Stock = 75 }
        );
        await context.SaveChangesAsync();
    }
}
