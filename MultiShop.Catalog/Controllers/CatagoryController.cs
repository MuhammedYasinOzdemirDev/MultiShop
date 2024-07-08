using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.Category;
using MultiShop.Catalog.Services.Category;

namespace MultiShop.Catalog.Controllers;

[ApiController]
[Route("api/Category")]
public class CatagoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CatagoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> CategoryList()
    {
        var values = await _categoryService.GetAllAsync();
        return Ok(values);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdCategory(string id)
    {
        var value = await _categoryService.GetByIdAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
    {
        await _categoryService.CreateAsync(dto);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
    {
        await _categoryService.UpdateAsync(dto.CategoryId,dto);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _categoryService.DeleteAsync(id);
        return Ok();
    }

}