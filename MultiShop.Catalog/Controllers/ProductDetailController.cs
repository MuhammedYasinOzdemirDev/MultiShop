using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetail;
using MultiShop.Catalog.Services.ProductDetail;

namespace MultiShop.Catalog.Controllers;

public class ProductDetailController : ControllerBase
{
    private readonly IProductDetailService _ProductDetailService;

    public ProductDetailController(IProductDetailService ProductDetailService)
    {
        _ProductDetailService = ProductDetailService;
    }

    [HttpGet]
    public async Task<IActionResult> ProductDetailList()
    {
        var values = await _ProductDetailService.GetAllAsync();
        return Ok(values);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdProductDetail(string id)
    {
        var value = await _ProductDetailService.GetByIdAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto dto)
    {
        await _ProductDetailService.CreateAsync(dto);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto dto)
    {
        await _ProductDetailService.UpdateAsync(dto.ProductDetailId,dto);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProductDetail(string id)
    {
        await _ProductDetailService.DeleteAsync(id);
        return Ok();
    }
}