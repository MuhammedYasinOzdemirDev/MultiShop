using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public interface IDiscountService
{
    Task<List<ResultCouponDto>> GetAllCouponAsync();
    Task CreateCouponAsync(CreateCouponDto dto);
    Task UpdateCouponAsync(UpdateCouponDto dto);
    Task DeleteCouponAsync(int id);
    Task<ResultCouponDto> GetByIdCouponAsync(int id);
}