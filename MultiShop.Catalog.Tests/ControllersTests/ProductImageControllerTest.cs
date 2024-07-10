using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MultiShop.Catalog.Controllers;
using MultiShop.Catalog.Dtos.ProductImage;
using MultiShop.Catalog.Services.ProductImage;
using Xunit;
using Assert = Xunit.Assert;


namespace MultiShop.Catalog.Tests.Controllers;
 public class ProductImageControllerTest
    {
        private readonly Mock<IProductImageService> _mockProductImageService;
        private readonly Mock<ILogger<ProductImageController>> _mockLogger;
        private readonly ProductImageController _controller;

        public ProductImageControllerTest()
        {
            _mockProductImageService = new Mock<IProductImageService>();
            _mockLogger = new Mock<ILogger<ProductImageController>>();
            _controller = new ProductImageController(_mockProductImageService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ProductImageList_ReturnsOkResult_WithAListOfProductImages()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var productImages = new List<ResultProductImageDto> { new ResultProductImageDto { ProductImageId = "1",Image1 = "http://example.com/image1.jpg" ,Image2 = "http://example.com/image2.jpg" ,Image3 = "http://example.com/image3.jpg" ,ProductId = "1"}};
            _mockProductImageService.Setup(service => service.GetAllAsync()).ReturnsAsync(productImages);

            // Act: Test edilen metodu çağır.
            var result = await _controller.ProductImageList();

            // Assert: Beklenen sonuçları doğrula.
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ResultProductImageDto>>(okResult.Value);
            returnValue.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByIdProductImage_ReturnsNotFound_WhenProductImageDoesNotExist()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockProductImageService.Setup(service => service.GetByIdAsync("1")).ReturnsAsync((ResultProductImageDto)null);

            // Act: Test edilen metodu çağır.
            var result = await _controller.GetByIdProductImage("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdProductImage_ReturnsOkResult_WhenProductImageExists()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var productImage = new ResultProductImageDto { ProductImageId = "1" ,Image1 = "http://example.com/image1.jpg" ,Image2 = "http://example.com/image2.jpg" ,Image3 = "http://example.com/image3.jpg" ,ProductId = "1"};
            _mockProductImageService.Setup(service => service.GetByIdAsync("1")).ReturnsAsync(productImage);

            // Act: Test edilen metodu çağır.
            var result = await _controller.GetByIdProductImage("1");

            // Assert: Beklenen sonuçları doğrula.
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResultProductImageDto>(okResult.Value);
            returnValue.ProductImageId.Should().Be("1");
        }

        [Fact]
        public async Task CreateProductImage_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act: Test edilen metodu çağır.
            var result = await _controller.CreateProductImage(null);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateProductImage_ReturnsOkResult_WhenDtoIsValid()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var dto = new CreateProductImageDto {  Image1 = "http://example.com/image1.jpg" ,Image2 = "http://example.com/image2.jpg" ,Image3 = "http://example.com/image3.jpg" ,ProductId = "1"};

            // Act: Test edilen metodu çağır.
            var result = await _controller.CreateProductImage(dto);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateProductImage_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act: Test edilen metodu çağır.
            var result = await _controller.UpdateProductImage(null);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProductImage_ReturnsOkResult_WhenDtoIsValid()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var dto = new UpdateProductImageDto { ProductImageId = "1", Image1 = "http://example.com/image1.jpg" ,Image2 = "http://example.com/image2.jpg" ,Image3 = "http://example.com/image3.jpg" ,ProductId = "1"};

            // Act: Test edilen metodu çağır.
            var result = await _controller.UpdateProductImage(dto);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProductImage_ReturnsOkResult()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockProductImageService.Setup(service => service.DeleteAsync("1")).Returns(Task.CompletedTask);

            // Act: Test edilen metodu çağır.
            var result = await _controller.DeleteProductImage("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProductImage_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockProductImageService.Setup(service => service.DeleteAsync("1")).Throws(new System.Exception());

            // Act: Test edilen metodu çağır.
            var result = await _controller.DeleteProductImage("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }
    }
