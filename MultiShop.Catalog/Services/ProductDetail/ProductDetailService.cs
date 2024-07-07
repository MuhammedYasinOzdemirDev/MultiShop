using AutoMapper;
using MultiShop.Catalog.Dtos.ProductDetail;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetail;

public class ProductDetailService:GenericService<Entities.ProductDetail,CreateProductDetailDto,ResultProductDetailDto,UpdateProductDetailDto>,IProductDetailService
{
    public ProductDetailService(IMapper mapper, IDatabaseSettings settings) : base(mapper, settings)
    {
    }
}