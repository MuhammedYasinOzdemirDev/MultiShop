using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MultiShop.Catalog.Controllers;
using MultiShop.Catalog.Dtos.Product;
using MultiShop.Catalog.Services.Product;
using Xunit;
using Assert = Xunit.Assert;

namespace MultiShop.Catalog.Tests.Controllers;

  public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ILogger<ProductController>> _mockLogger;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockLogger = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_mockProductService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ProductList_ReturnsOkResult_WithAListOfProducts()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var products = new List<ResultProductDto> { new ResultProductDto { ProductId = "1", ProductName = "Product1" } };
            _mockProductService.Setup(service => service.GetAllAsync()).ReturnsAsync(products);

            // Act: Test edilen metodu çağır.
            var result = await _controller.ProductList();

            // Assert: Beklenen sonuçları doğrula.
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ResultProductDto>>(okResult.Value);
            returnValue.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByIdProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockProductService.Setup(service => service.GetByIdAsync("1")).ReturnsAsync((ResultProductDto)null);

            // Act: Test edilen metodu çağır.
            var result = await _controller.GetByIdProduct("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdProduct_ReturnsOkResult_WhenProductExists()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var product = new ResultProductDto { ProductId = "1", ProductName = "Product1" };
            _mockProductService.Setup(service => service.GetByIdAsync("1")).ReturnsAsync(product);

            // Act: Test edilen metodu çağır.
            var result = await _controller.GetByIdProduct("1");

            // Assert: Beklenen sonuçları doğrula.
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResultProductDto>(okResult.Value);
            returnValue.ProductId.Should().Be("1");
        }

        [Fact]
        public async Task CreateProduct_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act: Test edilen metodu çağır.
            var result = await _controller.CreateProduct(null);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateProduct_ReturnsOkResult_WhenDtoIsValid()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var dto = new CreateProductDto { ProductName = "New Product" };

            // Act: Test edilen metodu çağır.
            var result = await _controller.CreateProduct(dto);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act: Test edilen metodu çağır.
            var result = await _controller.UpdateProduct(null);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOkResult_WhenDtoIsValid()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var dto = new UpdateProductDto { ProductId = "1", ProductName = "Updated Product" };

            // Act: Test edilen metodu çağır.
            var result = await _controller.UpdateProduct(dto);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOkResult()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockProductService.Setup(service => service.DeleteAsync("1")).Returns(Task.CompletedTask);

            // Act: Test edilen metodu çağır.
            var result = await _controller.DeleteProduct("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockProductService.Setup(service => service.DeleteAsync("1")).Throws(new System.Exception());

            // Act: Test edilen metodu çağır.
            var result = await _controller.DeleteProduct("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }
    }
