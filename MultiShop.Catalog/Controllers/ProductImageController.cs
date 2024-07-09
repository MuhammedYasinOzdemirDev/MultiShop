using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImage;
using MultiShop.Catalog.Services.ProductImage;

namespace MultiShop.Catalog.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductImageController : Controller
{
    private readonly IProductImageService _ProductImageService;

    public ProductImageController(IProductImageService ProductImageService)
    {
        _ProductImageService = ProductImageService;
    }

    [HttpGet]
    public async Task<IActionResult> ProductImageList()
    {
        var values = await _ProductImageService.GetAllAsync();
        return Ok(values);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdProductImage(string id)
    {
        var value = await _ProductImageService.GetByIdAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductImage(CreateProductImageDto dto)
    {
        await _ProductImageService.CreateAsync(dto);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto dto)
    {
        await _ProductImageService.UpdateAsync(dto.ProductImageId,dto);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProductImage(string id)
    {
        await _ProductImageService.DeleteAsync(id);
        return Ok();
    }
}