using MultiShop.Catalog.Dtos.ProductDetail;

namespace MultiShop.Catalog.Services.ProductDetail;

public interface IProductDetailService:IGenericService<Entities.ProductDetail,CreateProductDetailDto,ResultProductDetailDto,UpdateProductDetailDto>
{
    
}