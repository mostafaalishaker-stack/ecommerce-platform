namespace ECommerce.DTOs;

public record ProductResponse(int Id, string Name, string Description, decimal Price, string ImageUrl, string Category, int Stock);
public record CreateProductRequest(string Name, string Description, decimal Price, string ImageUrl, string Category, int Stock);
