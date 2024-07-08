using MultiShop.Catalog.Dtos.Product;

namespace MultiShop.Catalog.Services.Product;

public interface IProductService:IGenericService<Entities.Product,CreateProductDto,ResultProductDto,UpdateProductDto>
{
    
}