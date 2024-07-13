namespace MultiShop.Order.Domain.Entities;

public class Ordering
{
    public int OrderingId { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}