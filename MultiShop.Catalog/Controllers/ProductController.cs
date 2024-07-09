using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.Product;
using MultiShop.Catalog.Services.Product;

namespace MultiShop.Catalog.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _ProductService;
    private readonly ILogger<ProductController> _logger;
    public ProductController(IProductService ProductService,ILogger<ProductController> logger)
    {
        _ProductService = ProductService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> ProductList()
    {
        try
        {
            var values = await _ProductService.GetAllAsync();
            _logger.LogInformation("Product list retrieved successfully.");
            return Ok(values);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(ProductList)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdProduct(string id)
    {
        try
        {
            var value = await _ProductService.GetByIdAsync(id);
            if (value == null)
            {
                _logger.LogWarning($"Product with id {id} not found.");
                return NotFound("Product not found.");
            }
            _logger.LogInformation($"Product with id {id} retrieved successfully.");
            return Ok(value);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetByIdProduct)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("Invalid product data.");
                return BadRequest("Invalid product data.");
            }
            await _ProductService.CreateAsync(dto);
            _logger.LogInformation("Product created successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(CreateProduct)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("Invalid product data.");
                return BadRequest("Invalid product data.");
            }
            await _ProductService.UpdateAsync(dto.ProductId, dto);
            _logger.LogInformation($"Product with id {dto.ProductId} updated successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(UpdateProduct)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        try
        {
            await _ProductService.DeleteAsync(id);
            _logger.LogInformation($"Product with id {id} deleted successfully.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(DeleteProduct)}: {ex.Message}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
}