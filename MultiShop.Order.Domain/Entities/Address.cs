namespace MultiShop.Order.Domain.Entities;

public class Address
{
    public int AddressId { get; set; }
    public int UserId { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Detail { get; set; }
}