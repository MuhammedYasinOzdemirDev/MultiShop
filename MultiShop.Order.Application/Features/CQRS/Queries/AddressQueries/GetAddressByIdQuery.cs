namespace MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

{
    public int Id { get; set; }

    public GetAddressByIdQuery(int id)
    {
        Id = id;
    }
}