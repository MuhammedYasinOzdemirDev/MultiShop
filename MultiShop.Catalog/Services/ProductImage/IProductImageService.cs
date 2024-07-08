using MultiShop.Catalog.Dtos.ProductImage;

namespace MultiShop.Catalog.Services.ProductImage;

public interface IProductImageService:IGenericService<Entities.ProductImage,CreateProductImageDto,ResultProductImageDto,UpdateProductImageDto>
{
    
}