using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services;

public class GenericService <TEntity,CreateDto,ResultDto,UpdateDto> :IGenericService<TEntity,CreateDto,ResultDto,UpdateDto>
    where TEntity : class
    where CreateDto : class
    where ResultDto : class
    where UpdateDto : class
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<TEntity> _collection;
    private readonly ILogger<GenericService<TEntity, CreateDto, ResultDto, UpdateDto>> _logger;
    private readonly IMongoClient _client;

    public GenericService(IMapper mapper, IDatabaseSettings settings, ILogger<GenericService<TEntity, CreateDto, ResultDto, UpdateDto>> logger, IMongoClient client = null)
    {
        _mapper = mapper;
        _logger = logger;
        _client = client ?? new MongoClient("mongodb://localhost:27017");
        var database = _client.GetDatabase(settings.DatabaseName ?? "IntegrationTestDatabase");
        _collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity), settings));
    }

    private string GetCollectionName(Type entityType, IDatabaseSettings settings)
    {
        if (entityType == typeof(Entities.Category))
        {
            return settings.CategoryCollectionName;
        }
        else if (entityType == typeof(Entities.Product))
        {
            return settings.ProductCollectionName;
        }
        else if (entityType == typeof(Entities.ProductDetail))
        {
            return settings.ProductDetailCollectionName;
        }
        else if (entityType == typeof(Entities.ProductImage))
        {
            return settings.ProductImageCollectionName;
        }
        else
        {
            throw new ArgumentException("Collection name not found for the given entity type.");
        }
    }
    public async Task<IEnumerable<ResultDto>> GetAllAsync()
    {
        try
        {
            var values = await _collection.Find(x => true).ToListAsync();
            _logger.LogInformation($"{values.Count} entities retrieved from {typeof(TEntity).Name} collection.");
            return values.Select(x => _mapper.Map<ResultDto>(x));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetAllAsync)}: {ex.Message}", ex);
            throw;
        }
    }

    public async Task<ResultDto> GetByIdAsync(string id)
    {
        try
        {
        var filter = GetFilterById(id);
        var value =await _collection.Find(filter).FirstOrDefaultAsync();
        if (value == null) 
        {
            _logger.LogWarning($"Entity with id {id} not found in {typeof(TEntity).Name} collection.");
            throw new Exception("Entity not found.");
        }
        _logger.LogInformation($"Entity with id {id} retrieved from {typeof(TEntity).Name} collection.");
        return _mapper.Map<ResultDto>(value);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetByIdAsync)}: {ex.Message}", ex);
            throw;
        }
    }

    public async Task CreateAsync(CreateDto dto)
    {
        try
        {
        var value = _mapper.Map<TEntity>(dto);
        await _collection.InsertOneAsync(value);
        _logger.LogInformation($"Entity created in {typeof(TEntity).Name} collection.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(CreateAsync)}: {ex.Message}", ex);
            throw;
        }
    }

    public async Task UpdateAsync( string id,UpdateDto dto)
    {
        try
        {
            var filter = GetFilterById(id);
            var existingEntity = await _collection.Find(filter).FirstOrDefaultAsync();

            if (existingEntity == null)
            {
                _logger.LogWarning($"Entity with id {id} not found in {typeof(TEntity).Name} collection.");
                throw new Exception("Entity not found.");
            }

            _mapper.Map(dto, existingEntity); // Dtodan gelen veriler mevcut veriler guncellenir
            var updateResult = await _collection.ReplaceOneAsync(filter, existingEntity);

            if (updateResult.MatchedCount == 0)
            {
                _logger.LogWarning($"Entity with id {id} not updated in {typeof(TEntity).Name} collection.");
                throw new Exception("Entity not found.");
            }

            _logger.LogInformation($"Entity with id {id} updated in {typeof(TEntity).Name} collection.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(UpdateAsync)}: {ex.Message}", ex);
            throw;
        }
    }

    public async Task DeleteAsync(string id)
    {
        try
        {
        var filter = GetFilterById(id);
        var deleteResult= await _collection.DeleteOneAsync(id);
         if (deleteResult.DeletedCount == 0)
         {
             _logger.LogWarning($"Entity with id {id} not found in {typeof(TEntity).Name} collection.");
             throw new Exception("Entity not found.");
         }
         _logger.LogInformation($"Entity with id {id} deleted from {typeof(TEntity).Name} collection.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(DeleteAsync)}: {ex.Message}", ex);
            throw;
        }
    }
    private FilterDefinition<TEntity> GetFilterById(string id)
    {
        if (typeof(TEntity) == typeof(Entities.Category))
        {
            return Builders<TEntity>.Filter.Eq("CategoryId", id);
        }
        else if (typeof(TEntity) == typeof(Entities.Product))
        {
            return Builders<TEntity>.Filter.Eq("ProductId", id);
        }
        else if (typeof(TEntity) == typeof(Entities.ProductDetail))
        {
            return Builders<TEntity>.Filter.Eq("ProductDetailId", id);
        }
        else if (typeof(TEntity) == typeof(Entities.ProductImage))
        {
            return Builders<TEntity>.Filter.Eq("ProductImageId", id);
        }
        else
        {
            throw new ArgumentException("Filter not defined for the given entity type.");
        }
    }
}