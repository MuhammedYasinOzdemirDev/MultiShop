using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImage;
using MultiShop.Catalog.Services.ProductImage;

namespace MultiShop.Catalog.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductImageController : Controller
{
    private readonly IProductImageService _ProductImageService;
    private readonly ILogger<ProductImageController> _logger;
    public ProductImageController(IProductImageService ProductImageService,ILogger<ProductImageController> logger)
    {
        _ProductImageService = ProductImageService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> ProductImageList()
    {
        try
        {
            var values = await _ProductImageService.GetAllAsync();
            _logger.LogInformation("Product image list retrieved successfully.");
            return Ok(values);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(ProductImageList)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdProductImage(string id)
    {
        try
        {
            var value = await _ProductImageService.GetByIdAsync(id);
            if (value == null)
            {
                _logger.LogWarning($"Product image with id {id} not found.");
                return NotFound("Product image not found.");
            }
            _logger.LogInformation($"Product image with id {id} retrieved successfully.");
            return Ok(value);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetByIdProductImage)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductImage(CreateProductImageDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("Invalid product image data.");
                return BadRequest("Invalid product image data.");
            }
            await _ProductImageService.CreateAsync(dto);
            _logger.LogInformation("Product image created successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(CreateProductImage)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("Invalid product image data.");
                return BadRequest("Invalid product image data.");
            }
            await _ProductImageService.UpdateAsync(dto.ProductImageId, dto);
            _logger.LogInformation($"Product image with id {dto.ProductImageId} updated successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(UpdateProductImage)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProductImage(string id)
    {
        try
        {
            await _ProductImageService.DeleteAsync(id);
            _logger.LogInformation($"Product image with id {id} deleted successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(DeleteProductImage)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
}