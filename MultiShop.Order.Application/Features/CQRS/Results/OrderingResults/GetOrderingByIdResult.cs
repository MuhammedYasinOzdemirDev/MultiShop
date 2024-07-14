using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Results;

public class GetOrderingByIdResult
{
    public int OrderingId { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
}