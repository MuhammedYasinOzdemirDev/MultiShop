using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public class DiscountService:IDiscountService
{
    private readonly DapperContext _context;
 private readonly ILogger<DiscountService> _logger;

    public DiscountService(DapperContext context, ILogger<DiscountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ResultCouponDto>> GetAllCouponAsync()
    {
        var query = "SELECT * FROM Coupons";

        try
        {
            _logger.LogInformation("Fetching all coupons from database.");
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCouponDto>(query);
                _logger.LogInformation("Fetched {Count} coupons from database.", values.Count());
                return values.ToList();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all coupons.");
            throw;
        }
    }

    public async Task CreateCouponAsync(CreateCouponDto dto)
    {
        var query = "INSERT INTO Coupons (Code, Rate, IsActive, ValidDate) VALUES (@code, @rate, @isActive, @validDate)";
        var parameters = new DynamicParameters();
        parameters.Add("@code", dto.Code);
        parameters.Add("@rate", dto.Rate);
        parameters.Add("@isActive", dto.IsActive);
        parameters.Add("@validDate", dto.ValidDate);

        try
        {
            _logger.LogInformation("Creating a new coupon with code {Code}.", dto.Code);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                _logger.LogInformation("Coupon with code {Code} created successfully.", dto.Code);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new coupon with code {Code}.", dto.Code);
            throw;
        }
    }

    public async Task UpdateCouponAsync(UpdateCouponDto dto)
    {
        var query = "UPDATE Coupons SET Code=@code, Rate=@rate, IsActive=@isActive, ValidDate=@validDate WHERE CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@code", dto.Code);
        parameters.Add("@rate", dto.Rate);
        parameters.Add("@isActive", dto.IsActive);
        parameters.Add("@validDate", dto.ValidDate);
        parameters.Add("@couponId", dto.CouponId);

        try
        {
            _logger.LogInformation("Updating coupon with ID {CouponId}.", dto.CouponId);
            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, parameters);
                if (rowsAffected > 0)
                {
                    _logger.LogInformation("Coupon with ID {CouponId} updated successfully.", dto.CouponId);
                }
                else
                {
                    _logger.LogWarning("Coupon with ID {CouponId} not found for update.", dto.CouponId);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating coupon with ID {CouponId}.", dto.CouponId);
            throw;
        }
    }

    public async Task DeleteCouponAsync(int id)
    {
        var query = "DELETE FROM Coupons WHERE CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@couponId", id);

        try
        {
            _logger.LogInformation("Deleting coupon with ID {CouponId}.", id);
            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, parameters);
                if (rowsAffected > 0)
                {
                    _logger.LogInformation("Coupon with ID {CouponId} deleted successfully.", id);
                }
                else
                {
                    _logger.LogWarning("Coupon with ID {CouponId} not found for deletion.", id);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting coupon with ID {CouponId}.", id);
            throw;
        }
    }

    public async Task<ResultCouponDto> GetByIdCouponAsync(int id)
    {
        var query = "SELECT * FROM Coupons WHERE CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@couponId", id);

        try
        {
            _logger.LogInformation("Fetching coupon with ID {CouponId} from database.", id);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<ResultCouponDto>(query, parameters);
                if (value == null)
                {
                    _logger.LogWarning("Coupon with ID {CouponId} not found.", id);
                }
                else
                {
                    _logger.LogInformation("Coupon with ID {CouponId} fetched successfully.", id);
                }
                return value;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching coupon with ID {CouponId}.", id);
            throw;
        }
    }
}