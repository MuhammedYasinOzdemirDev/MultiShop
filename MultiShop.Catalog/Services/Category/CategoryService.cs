using AutoMapper;
using MultiShop.Catalog.Dtos.Category;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.Category;

public class CategoryService:GenericService<Entities.Category,CreateCategoryDto,ResultCategoryDto,UpdateCategoryDto>,ICategoryService
{
    public CategoryService(IMapper mapper, IDatabaseSettings settings) : base(mapper, settings)
    {
    }
}