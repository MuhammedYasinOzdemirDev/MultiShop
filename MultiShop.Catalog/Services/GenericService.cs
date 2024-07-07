using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services;

public class GenericService <TEntity,CreateDto,ResultDto,UpdateDto> :IGenericService<CreateDto,ResultDto,UpdateDto>
    where TEntity : class
    where CreateDto : class
    where ResultDto : class
    where UpdateDto : class
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<TEntity> _collection;
    public GenericService(IMapper mapper,IDatabaseSettings settings)
    {
        _mapper = mapper;
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
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
       var values= await _collection.Find(x => true).ToListAsync();
       return values.Select(x => _mapper.Map<ResultDto>(x));
    }

    public async Task<ResultDto> GetByIdAsync(string id)
    {
        var filter = GetFilterById(id);
        var value = _collection.Find(filter).FirstOrDefaultAsync();
        return _mapper.Map<ResultDto>(value);
    }

    public async Task CreateAsync(CreateDto dto)
    {
        var value = _mapper.Map<TEntity>(dto);
        await _collection.InsertOneAsync(value);
    }

    public async Task UpdateAsync( string id,UpdateDto dto)
    {
        var filter = GetFilterById(id);
        var update = _mapper.Map<TEntity>(dto);
        
     
        var existingEntity = await _collection.Find(filter).FirstOrDefaultAsync();
        if (existingEntity == null) throw new KeyNotFoundException("Entity not found");

        _mapper.Map(dto, existingEntity); //Dtodan gelen veriler mevcut veriler guncellenir
        var updateResult = await _collection.ReplaceOneAsync(filter, existingEntity);
        
        if (updateResult.MatchedCount == 0)
        {
            throw new KeyNotFoundException("Entity not found");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var filter = GetFilterById(id);
         await _collection.DeleteOneAsync(id);
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