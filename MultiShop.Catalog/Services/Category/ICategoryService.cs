using MultiShop.Catalog.Dtos.Category;

namespace MultiShop.Catalog.Services.Category;

public interface ICategoryService:IGenericService<Entities.Category,CreateCategoryDto,ResultCategoryDto,UpdateCategoryDto>
{
    
}