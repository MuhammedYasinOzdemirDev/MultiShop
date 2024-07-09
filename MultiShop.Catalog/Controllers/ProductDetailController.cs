using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetail;
using MultiShop.Catalog.Services.ProductDetail;

namespace MultiShop.Catalog.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductDetailController : ControllerBase
{
    private readonly IProductDetailService _ProductDetailService;
    private readonly ILogger<ProductDetailController> _logger;

    public ProductDetailController(IProductDetailService ProductDetailService, ILogger<ProductDetailController> logger)
    {
        _ProductDetailService = ProductDetailService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> ProductDetailList()
    {
        try
        {
            var values = await _ProductDetailService.GetAllAsync();
            _logger.LogInformation("Product detail list retrieved successfully.");
            return Ok(values);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(ProductDetailList)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdProductDetail(string id)
    {
        try
        {
            var value = await _ProductDetailService.GetByIdAsync(id);
            if (value == null)
            {
                _logger.LogWarning($"Product detail with id {id} not found.");
                return NotFound("Product detail not found.");
            }
            _logger.LogInformation($"Product detail with id {id} retrieved successfully.");
            return Ok(value);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetByIdProductDetail)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("Invalid product detail data.");
                return BadRequest("Invalid product detail data.");
            }
            await _ProductDetailService.CreateAsync(dto);
            _logger.LogInformation("Product detail created successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(CreateProductDetail)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("Invalid product detail data.");
                return BadRequest("Invalid product detail data.");
            }
            await _ProductDetailService.UpdateAsync(dto.ProductDetailId, dto);
            _logger.LogInformation($"Product detail with id {dto.ProductDetailId} updated successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(UpdateProductDetail)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProductDetail(string id)
    {
        try
        {
            await _ProductDetailService.DeleteAsync(id);
            _logger.LogInformation($"Product detail with id {id} deleted successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(DeleteProductDetail)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
}