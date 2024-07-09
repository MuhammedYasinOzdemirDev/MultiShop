using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using MultiShop.Catalog.Dtos.Category;
using MultiShop.Catalog.Services;
using MultiShop.Catalog.Settings;
using Xunit;

namespace MultiShop.Catalog.Tests;

    public class GenericServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IMongoCollection<Entities.Category>> _mockCollection;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly Mock<IMongoClient> _mockClient;
        private readonly Mock<ILogger<GenericService<Entities.Category, CreateCategoryDto, ResultCategoryDto, UpdateCategoryDto>>> _mockLogger;
        private readonly IDatabaseSettings _databaseSettings;
        private readonly GenericService<Entities.Category, CreateCategoryDto, ResultCategoryDto, UpdateCategoryDto> _service;

        public GenericServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockCollection = new Mock<IMongoCollection<Entities.Category>>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
            _mockLogger = new Mock<ILogger<GenericService<Entities.Category, CreateCategoryDto, ResultCategoryDto, UpdateCategoryDto>>>();

            // Create an instance of DatabaseSettings with non-null properties
            _databaseSettings = new DatabaseSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestDatabase",
                CategoryCollectionName = "Categories",
                ProductCollectionName = "Products",
                ProductDetailCollectionName = "ProductDetails",
                ProductImageCollectionName = "ProductImages"
            };

            // Setup MongoDB client and database mocks
            _mockClient.Setup(c => c.GetDatabase(_databaseSettings.DatabaseName, null))
                .Returns(_mockDatabase.Object);
            _mockDatabase.Setup(d => d.GetCollection<Entities.Category>(_databaseSettings.CategoryCollectionName, null))
                .Returns(_mockCollection.Object);

            _service = new GenericService<Entities.Category, CreateCategoryDto, ResultCategoryDto, UpdateCategoryDto>(
                _mockMapper.Object, _databaseSettings, _mockLogger.Object, _mockClient.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedResult()
        {
            // Arrange
            var entities = new List<Entities.Category> { new Entities.Category { CategoryId = "1", CategoryName = "Category1" } };

            var mockAsyncCursor = new Mock<IAsyncCursor<Entities.Category>>();
            mockAsyncCursor.Setup(_ => _.Current).Returns(entities);
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            _mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Entities.Category>>(), It.IsAny<FindOptions<Entities.Category>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockAsyncCursor.Object);

            _mockMapper.Setup(m => m.Map<ResultCategoryDto>(It.IsAny<Entities.Category>()))
                .Returns(new ResultCategoryDto { CategoryId = "1", CategoryName = "Category1" });

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().HaveCount(1);
            result.First().CategoryId.Should().Be("1");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedResult()
        {
            // Arrange
            var entity = new Entities.Category { CategoryId = "1", CategoryName = "Category1" };
            var entities = new List<Entities.Category> { entity };

            var mockAsyncCursor = new Mock<IAsyncCursor<Entities.Category>>();
            mockAsyncCursor.Setup(_ => _.Current).Returns(entities);
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            _mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Entities.Category>>(), It.IsAny<FindOptions<Entities.Category>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockAsyncCursor.Object);

            _mockMapper.Setup(m => m.Map<ResultCategoryDto>(It.IsAny<Entities.Category>()))
                .Returns(new ResultCategoryDto { CategoryId = "1", CategoryName = "Category1" });

            // Act
            var result = await _service.GetByIdAsync("1");

            // Assert
            result.CategoryId.Should().Be("1");
        }

        [Fact]
        public async Task CreateAsync_InvokesCollectionInsertOneAsync()
        {
            // Arrange
            var createDto = new CreateCategoryDto { CategoryName = "Category1" };
            var entity = new Entities.Category { CategoryId = "1", CategoryName = "Category1" };

            _mockMapper.Setup(m => m.Map<Entities.Category>(It.IsAny<CreateCategoryDto>())).Returns(entity);

            // Act
            await _service.CreateAsync(createDto);

            // Assert
            _mockCollection.Verify(c => c.InsertOneAsync(entity, null, default), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvokesCollectionReplaceOneAsync()
        {
            // Arrange
            var updateDto = new UpdateCategoryDto { CategoryId = "1", CategoryName = "UpdatedCategory" };
            var entity = new Entities.Category { CategoryId = "1", CategoryName = "Category1" };

            var mockAsyncCursor = new Mock<IAsyncCursor<Entities.Category>>();
            mockAsyncCursor.Setup(_ => _.Current).Returns(new List<Entities.Category> { entity });
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            _mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Entities.Category>>(), It.IsAny<FindOptions<Entities.Category>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockAsyncCursor.Object);

            _mockMapper.Setup(m => m.Map(updateDto, entity)).Returns(entity);

            _mockCollection.Setup(c => c.ReplaceOneAsync(It.IsAny<FilterDefinition<Entities.Category>>(), entity, It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ReplaceOneResult.Acknowledged(1, 1, new BsonObjectId(new ObjectId())));

            // Act
            await _service.UpdateAsync("1", updateDto);

            // Assert
            _mockCollection.Verify(c => c.ReplaceOneAsync(It.IsAny<FilterDefinition<Entities.Category>>(), entity, It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task DeleteAsync_InvokesCollectionDeleteOneAsync()
        {
            // Arrange
            _mockCollection.Setup(c => c.DeleteOneAsync(It.IsAny<FilterDefinition<Entities.Category>>(), default))
                .ReturnsAsync(new DeleteResult.Acknowledged(1));

            // Act
            await _service.DeleteAsync("1");

            // Assert
            _mockCollection.Verify(c => c.DeleteOneAsync(It.IsAny<FilterDefinition<Entities.Category>>(), default), Times.Once);
        }
    }
