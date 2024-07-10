using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.Category;
using MultiShop.Catalog.Services.Category;

namespace MultiShop.Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;
    public CategoryController(ICategoryService categoryService,ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> CategoryList()
    {
        try
        {
          
            var values = await _categoryService.GetAllAsync();
            _logger.LogInformation("Category list retrieved successfully.");
            return Ok(values);
        }catch(Exception ex){ 
            _logger.LogError($"Error in {nameof(CategoryList)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error"); 
            }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCategory(string id)
    {
        try
        {
            var value = await _categoryService.GetByIdAsync(id);
            if (value == null)
            {
                _logger.LogWarning($"Category with id {id} not found.");
                return NotFound("Category not found.");
            }

            _logger.LogInformation($"Category with id {id} retrieved successfully.");
            return Ok(value);
        }  catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetByIdCategory)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid category data.");
                return BadRequest(ModelState);
            }
            if (dto == null)
            {
                _logger.LogWarning("Invalid category data.");
                return BadRequest("Invalid category data.");
            }
            await _categoryService.CreateAsync(dto);
            _logger.LogInformation("Category created successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(CreateCategory)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("Invalid category data.");
                return BadRequest("Invalid category data.");
            }
            await _categoryService.UpdateAsync(dto.CategoryId, dto);
            _logger.LogInformation($"Category with id {dto.CategoryId} updated successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(UpdateCategory)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        try
        {
            await _categoryService.DeleteAsync(id);
            _logger.LogInformation($"Category with id {id} deleted successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(DeleteCategory)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
}