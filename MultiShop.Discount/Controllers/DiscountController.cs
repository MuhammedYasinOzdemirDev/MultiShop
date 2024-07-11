using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService service)
    {
        _discountService = service;
    }

   [HttpGet]
    public async Task<IActionResult> GetDiscountListAsync()
    {
        try
        {
            var values = await _discountService.GetAllCouponAsync();
            return Ok(values);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdDiscountAsync(int id)
    {
        try
        {
            var value = await _discountService.GetByIdCouponAsync(id);
            if (value == null)
            {
                return NotFound($"Coupon with ID {id} not found.");
            }
            return Ok(value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscountAsync(CreateCouponDto dto)
    {
        try
        {
            if (dto == null)
            {
                return BadRequest("Coupon data is null.");
            }

            await _discountService.CreateCouponAsync(dto);
            return Ok("Coupon successfully added.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDiscountAsync(UpdateCouponDto dto)
    {
        try
        {
            if (dto == null)
            {
                return BadRequest("Coupon data is null.");
            }

            var existingCoupon = await _discountService.GetByIdCouponAsync(dto.CouponId);
            if (existingCoupon == null)
            {
                return NotFound($"Coupon with ID {dto.CouponId} not found.");
            }

            await _discountService.UpdateCouponAsync(dto);
            return Ok("Coupon successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscountAsync(int id)
    {
        try
        {
            var existingCoupon = await _discountService.GetByIdCouponAsync(id);
            if (existingCoupon == null)
            {
                return NotFound($"Coupon with ID {id} not found.");
            }

            await _discountService.DeleteCouponAsync(id);
            return Ok("Coupon successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}