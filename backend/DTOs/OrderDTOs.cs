namespace ECommerce.DTOs;

public record CreateOrderRequest(string ShippingAddress, List<CartItem> Items);
public record CartItem(int ProductId, int Quantity);
public record OrderResponse(int Id, DateTime CreatedAt, decimal Total, string Status, List<OrderItemResponse> Items);
public record OrderItemResponse(string ProductName, decimal Price, int Quantity);
