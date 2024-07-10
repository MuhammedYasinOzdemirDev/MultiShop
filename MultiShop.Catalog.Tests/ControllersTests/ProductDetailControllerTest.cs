using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MultiShop.Catalog.Controllers;
using MultiShop.Catalog.Dtos.ProductDetail;
using MultiShop.Catalog.Services.ProductDetail;
using Xunit;
using Assert = Xunit.Assert;

namespace MultiShop.Catalog.Tests.Controllers;

 public class ProductDetailControllerTests
    {
        private readonly Mock<IProductDetailService> _mockProductDetailService;
        private readonly Mock<ILogger<ProductDetailController>> _mockLogger;
        private readonly ProductDetailController _controller;

        public ProductDetailControllerTests()
        {
            _mockProductDetailService = new Mock<IProductDetailService>();
            _mockLogger = new Mock<ILogger<ProductDetailController>>();
            _controller = new ProductDetailController(_mockProductDetailService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ProductDetailList_ReturnsOkResult_WithAListOfProductDetails()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var productDetails = new List<ResultProductDetailDto> { new ResultProductDetailDto { ProductDetailId = "1", ProductDescription = "Detail1" ,ProductInfo = "ds",ProductId = "1"} };
            _mockProductDetailService.Setup(service => service.GetAllAsync()).ReturnsAsync(productDetails);

            // Act: Test edilen metodu çağır.
            var result = await _controller.ProductDetailList();

            // Assert: Beklenen sonuçları doğrula.
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ResultProductDetailDto>>(okResult.Value);
            returnValue.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByIdProductDetail_ReturnsNotFound_WhenProductDetailDoesNotExist()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockProductDetailService.Setup(service => service.GetByIdAsync("1")).ReturnsAsync((ResultProductDetailDto)null);

            // Act: Test edilen metodu çağır.
            var result = await _controller.GetByIdProductDetail("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdProductDetail_ReturnsOkResult_WhenProductDetailExists()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var productDetail = new ResultProductDetailDto { ProductDetailId = "1", ProductDescription = "Detail1" ,ProductInfo = "ds",ProductId = "1"};
            _mockProductDetailService.Setup(service => service.GetByIdAsync("1")).ReturnsAsync(productDetail);

            // Act: Test edilen metodu çağır.
            var result = await _controller.GetByIdProductDetail("1");

            // Assert: Beklenen sonuçları doğrula.
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResultProductDetailDto>(okResult.Value);
            returnValue.ProductDetailId.Should().Be("1");
        }

        [Fact]
        public async Task CreateProductDetail_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act: Test edilen metodu çağır.
            var result = await _controller.CreateProductDetail(null);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateProductDetail_ReturnsOkResult_WhenDtoIsValid()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var dto = new CreateProductDetailDto { ProductDescription = "New Detail" ,ProductInfo = "ds",ProductId = "1"};

            // Act: Test edilen metodu çağır.
            var result = await _controller.CreateProductDetail(dto);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateProductDetail_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act: Test edilen metodu çağır.
            var result = await _controller.UpdateProductDetail(null);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProductDetail_ReturnsOkResult_WhenDtoIsValid()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var dto = new UpdateProductDetailDto { ProductDetailId = "1", ProductDescription = "Updated Detail" ,ProductInfo = "ds",ProductId = "1"};

            // Act: Test edilen metodu çağır.
            var result = await _controller.UpdateProductDetail(dto);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProductDetail_ReturnsOkResult()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockProductDetailService.Setup(service => service.DeleteAsync("1")).Returns(Task.CompletedTask);

            // Act: Test edilen metodu çağır.
            var result = await _controller.DeleteProductDetail("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProductDetail_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockProductDetailService.Setup(service => service.DeleteAsync("1")).Throws(new System.Exception());

            // Act: Test edilen metodu çağır.
            var result = await _controller.DeleteProductDetail("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }
    }
