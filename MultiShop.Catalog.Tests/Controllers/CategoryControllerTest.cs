using FluentAssertions;

namespace MultiShop.Catalog.Tests.Controllers;

using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MultiShop.Catalog.Controllers;
using MultiShop.Catalog.Services.Category;
using MultiShop.Catalog.Dtos.Category;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<ILogger<CategoryController>> _mockLogger;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _mockLogger = new Mock<ILogger<CategoryController>>();
            _controller = new CategoryController(_mockCategoryService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CategoryList_ReturnsOkResult_WithAListOfCategories()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var categories = new List<ResultCategoryDto> { new ResultCategoryDto { CategoryId = "1", CategoryName = "Category1" } };
            _mockCategoryService.Setup(service => service.GetAllAsync()).ReturnsAsync(categories);

            // Act: Test edilen metodu çağır.
            var result = await _controller.CategoryList();

            // Assert: Beklenen sonuçları doğrula.
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ResultCategoryDto>>(okResult.Value);
            returnValue.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByIdCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockCategoryService.Setup(service => service.GetByIdAsync("1")).ReturnsAsync((ResultCategoryDto)null);

            // Act: Test edilen metodu çağır.
            var result = await _controller.GetByIdCategory("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdCategory_ReturnsOkResult_WhenCategoryExists()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var category = new ResultCategoryDto { CategoryId = "1", CategoryName = "Category1" };
            _mockCategoryService.Setup(service => service.GetByIdAsync("1")).ReturnsAsync(category);

            // Act: Test edilen metodu çağır.
            var result = await _controller.GetByIdCategory("1");

            // Assert: Beklenen sonuçları doğrula.
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResultCategoryDto>(okResult.Value);
            returnValue.CategoryId.Should().Be("1");
        }

        [Fact]
        public async Task CreateCategory_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var dto = new CreateCategoryDto { CategoryName = string.Empty };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act: Test edilen metodu çağır.
            var result = await _controller.CreateCategory(dto);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act: Test edilen metodu çağır.
            var result = await _controller.CreateCategory(null);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_ReturnsOkResult_WhenModelStateIsValid()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var dto = new CreateCategoryDto { CategoryName = "New Category" };

            // Act: Test edilen metodu çağır.
            var result = await _controller.CreateCategory(dto);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act: Test edilen metodu çağır.
            var result = await _controller.UpdateCategory(null);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsOkResult_WhenDtoIsValid()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            var dto = new UpdateCategoryDto { CategoryId = "1", CategoryName = "Updated Category" };

            // Act: Test edilen metodu çağır.
            var result = await _controller.UpdateCategory(dto);

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsOkResult()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockCategoryService.Setup(service => service.DeleteAsync("1")).Returns(Task.CompletedTask);

            // Act: Test edilen metodu çağır.
            var result = await _controller.DeleteCategory("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange: Test için gerekli verileri ve davranışları ayarla.
            _mockCategoryService.Setup(service => service.DeleteAsync("1")).Throws(new System.Exception());

            // Act: Test edilen metodu çağır.
            var result = await _controller.DeleteCategory("1");

            // Assert: Beklenen sonuçları doğrula.
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }
    }
