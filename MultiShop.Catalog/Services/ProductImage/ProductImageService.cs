using AutoMapper;
using MultiShop.Catalog.Dtos.ProductImage;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImage;

public class ProductImageService:GenericService<Entities.ProductImage,CreateProductImageDto,ResultProductImageDto,UpdateProductImageDto>,IProductImageService
{
    public ProductImageService(IMapper mapper, IDatabaseSettings settings) : base(mapper, settings)
    {
    }
}