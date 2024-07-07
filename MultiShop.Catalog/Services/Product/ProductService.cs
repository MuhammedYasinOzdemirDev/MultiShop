using AutoMapper;
using MultiShop.Catalog.Dtos.Product;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.Product;

public class ProductService:GenericService<Entities.Product, CreateProductDto, ResultProductDto, UpdateProductDto>, IProductService
{
    public ProductService(IMapper mapper, IDatabaseSettings settings) : base(mapper, settings)
    {
    }
}