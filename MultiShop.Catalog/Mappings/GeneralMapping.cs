using AutoMapper;
using MultiShop.Catalog.Dtos.Category;
using MultiShop.Catalog.Dtos.Product;
using MultiShop.Catalog.Dtos.ProductDetail;
using MultiShop.Catalog.Dtos.ProductImage;
using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Mappings;

public class GeneralMapping:Profile
{
    public GeneralMapping()
    {
        CreateMap<Category, ResultCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        
        CreateMap<Product, ResultProductDto>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        
        CreateMap<ProductDetail, ResultProductDetailDto>().ReverseMap();
        CreateMap<ProductDetail, CreateProductDetailDto>().ReverseMap();
        CreateMap<ProductDetail, UpdateProductDetailDto>().ReverseMap();
        
        CreateMap<ProductImage, ResultProductImageDto>().ReverseMap();
        CreateMap<ProductImage, CreateProductImageDto>().ReverseMap();
        CreateMap<ProductImage, UpdateProductImageDto>().ReverseMap();

    } 
}