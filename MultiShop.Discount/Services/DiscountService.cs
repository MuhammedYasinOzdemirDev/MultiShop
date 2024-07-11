using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public class DiscountService:IDiscountService
{
    private readonly DapperContext _context;

    public DiscountService(DapperContext context)
    {
        _context = context;
    }
    public async Task<List<ResultCouponDto>> GetAllCouponAsync()
    {
        var query = "Select * from Coupons";
        var parameters = new DynamicParameters();
       
        using (var connection=_context.CreateConnection())
        {
            var values= await connection.QueryAsync<ResultCouponDto>(query);
            return values.ToList();
        }
    }

    public async Task CreateCouponAsync(CreateCouponDto dto)
    {
        string query = "Insert Into Coupons (Code,Rate,IsActive,ValidDate) values(@code @rate,@isActive,validDate)";
        var parameters = new DynamicParameters();
        parameters.Add("@code",dto.Code);
        parameters.Add("@rate",dto.Rate);
        parameters.Add("@isActive",dto.IsActive);
        parameters.Add("@validDate",dto.ValidDate);
        using (var connection=_context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
            
        }
    }

    public async Task UpdateCouponAsync(UpdateCouponDto dto)
    {
        var query =
            "Update Coupons Set Code=@code ,Rate=@rate,IsActive=@isActive,ValidDate=@validDate where CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@code",dto.Code);
        parameters.Add("@rate",dto.Rate);
        parameters.Add("@isActive",dto.IsActive);
        parameters.Add("@validDate",dto.ValidDate);
        parameters.Add("@couponId",dto.CouponId);
        using (var connection=_context.CreateConnection())
        {
           await connection.ExecuteAsync(query, parameters);
        }

    }

    public async Task DeleteCouponAsync(int id)
    {
        var query = "Delete from Coupons Where CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@couponId",id);
        using (var connection=_context.CreateConnection())
        {
           await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task<ResultCouponDto> GetByIdCouponAsync(int id)
    {
        var query = "Select * from Coupons Where CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@couponId",id);
        using (var connection=_context.CreateConnection())
        {
           var value= await connection.QueryFirstOrDefaultAsync<ResultCouponDto>(query, parameters);
           return value;
        }
    }
}