namespace MultiShop.Catalog.Services;

public interface IGenericService<TEntity,CreateDto, ResultDto, UpdateDto>
{
    Task<IEnumerable<ResultDto>> GetAllAsync();
    Task<ResultDto> GetByIdAsync(string id);
    Task CreateAsync(CreateDto dto);
    Task UpdateAsync( string id,UpdateDto dto);
    Task DeleteAsync(string id);
}
