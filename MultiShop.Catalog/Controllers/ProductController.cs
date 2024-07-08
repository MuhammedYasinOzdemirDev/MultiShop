using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.Product;
using MultiShop.Catalog.Services.Product;

namespace MultiShop.Catalog.Controllers;

public class ProductController : ControllerBase
{
    private readonly IProductService _ProductService;

    public ProductController(IProductService ProductService)
    {
        _ProductService = ProductService;
    }

    [HttpGet]
    public async Task<IActionResult> ProductList()
    {
        var values = await _ProductService.GetAllAsync();
        return Ok(values);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdProduct(string id)
    {
        var value = await _ProductService.GetByIdAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto dto)
    {
        await _ProductService.CreateAsync(dto);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
    {
        await _ProductService.UpdateAsync(dto.ProductId,dto);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _ProductService.DeleteAsync(id);
        return Ok();
    }
}