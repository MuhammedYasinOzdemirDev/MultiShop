using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;
     private readonly ILogger<DiscountController> _logger;

        public DiscountController(IDiscountService service, ILogger<DiscountController> logger)
        {
            _discountService = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscountListAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all coupons.");
                var values = await _discountService.GetAllCouponAsync();
                _logger.LogInformation("Fetched {Count} coupons.", values.Count);
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all coupons.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdDiscountAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching coupon with ID {CouponId}.", id);
                var value = await _discountService.GetByIdCouponAsync(id);
                if (value == null)
                {
                    _logger.LogWarning("Coupon with ID {CouponId} not found.", id);
                    return NotFound($"Coupon with ID {id} not found.");
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching coupon with ID {CouponId}.", id);
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
                    _logger.LogWarning("Received null CreateCouponDto.");
                    return BadRequest("Coupon data is null.");
                }

                _logger.LogInformation("Creating a new coupon with code {Code}.", dto.Code);
                await _discountService.CreateCouponAsync(dto);
                _logger.LogInformation("Coupon with code {Code} created successfully.", dto.Code);
                return Ok("Coupon successfully added.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new coupon with code {Code}.", dto.Code);
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
                    _logger.LogWarning("Received null UpdateCouponDto.");
                    return BadRequest("Coupon data is null.");
                }

                _logger.LogInformation("Updating coupon with ID {CouponId}.", dto.CouponId);
                var existingCoupon = await _discountService.GetByIdCouponAsync(dto.CouponId);
                if (existingCoupon == null)
                {
                    _logger.LogWarning("Coupon with ID {CouponId} not found.", dto.CouponId);
                    return NotFound($"Coupon with ID {dto.CouponId} not found.");
                }

                await _discountService.UpdateCouponAsync(dto);
                _logger.LogInformation("Coupon with ID {CouponId} updated successfully.", dto.CouponId);
                return Ok("Coupon successfully updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating coupon with ID {CouponId}.", dto.CouponId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting coupon with ID {CouponId}.", id);
                var existingCoupon = await _discountService.GetByIdCouponAsync(id);
                if (existingCoupon == null)
                {
                    _logger.LogWarning("Coupon with ID {CouponId} not found.", id);
                    return NotFound($"Coupon with ID {id} not found.");
                }

                await _discountService.DeleteCouponAsync(id);
                _logger.LogInformation("Coupon with ID {CouponId} deleted successfully.", id);
                return Ok("Coupon successfully deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting coupon with ID {CouponId}.", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
