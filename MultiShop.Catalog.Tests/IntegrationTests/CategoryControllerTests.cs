using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MultiShop.Catalog.Dtos.Category;
using Xunit;
using Assert = Xunit.Assert;


namespace MultiShop.Catalog.Tests.IntegrationTests;

    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly ILogger<CategoryControllerTests> _logger;

        public CategoryControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost:5148")
            });
            _logger = factory.Services.GetRequiredService<ILogger<CategoryControllerTests>>();
        }

        [Fact]
        public async Task Category_CRUD_Test()
        {
            // Log before creating category
            _logger.LogInformation("Starting Category_CRUD_Test");

            // Create Category
            var createCategoryDto = new CreateCategoryDto { CategoryName = "Test Category" };
            var createContent = new StringContent(JsonSerializer.Serialize(createCategoryDto), Encoding.UTF8, "application/json");
            _logger.LogInformation($"Sending POST request to /api/Category with content: {await createContent.ReadAsStringAsync()}");

            var createResponse = await _client.PostAsync("/api/Category", createContent);

            if (createResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var responseContent = await createResponse.Content.ReadAsStringAsync();
                _logger.LogError($"Create Category failed: {responseContent}");
                throw new Xunit.Sdk.XunitException($"Expected createResponse.StatusCode to be HttpStatusCode.OK, but found {createResponse.StatusCode} with response: {responseContent}");
            }

            createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // Log after creating category
            _logger.LogInformation("Category created successfully");

            // Get All Categories
            var getAllResponse = await _client.GetAsync("/api/Category");
            _logger.LogInformation($"GET request to /api/Category returned status: {getAllResponse.StatusCode}");

            getAllResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var getAllCategories = JsonSerializer.Deserialize<List<ResultCategoryDto>>(await getAllResponse.Content.ReadAsStringAsync());
            getAllCategories.Should().NotBeNull();
            getAllCategories.Should().HaveCountGreaterThan(0);
            var categoryId = getAllCategories.First().CategoryId;

            // Log retrieved categories
            _logger.LogInformation($"Retrieved {getAllCategories.Count} categories");

            // Get Category by ID
            var getByIdResponse = await _client.GetAsync($"/api/Category/{categoryId}");
            _logger.LogInformation($"GET request to /api/Category/{categoryId} returned status: {getByIdResponse.StatusCode}");

            getByIdResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var getCategoryById = JsonSerializer.Deserialize<ResultCategoryDto>(await getByIdResponse.Content.ReadAsStringAsync());
            getCategoryById.Should().NotBeNull();

            // Log retrieved category by ID
            _logger.LogInformation($"Category with ID {categoryId} retrieved successfully");

            // Update Category
            var updateCategoryDto = new UpdateCategoryDto { CategoryId = categoryId, CategoryName = "Updated Category" };
            var updateContent = new StringContent(JsonSerializer.Serialize(updateCategoryDto), Encoding.UTF8, "application/json");
            _logger.LogInformation($"Sending PUT request to /api/Category with content: {await updateContent.ReadAsStringAsync()}");

            var updateResponse = await _client.PutAsync("/api/Category", updateContent);
            _logger.LogInformation($"PUT request to /api/Category returned status: {updateResponse.StatusCode}");

            updateResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // Log category update
            _logger.LogInformation($"Category with ID {categoryId} updated successfully");

            // Delete Category
            var deleteResponse = await _client.DeleteAsync($"/api/Category/{categoryId}");
            _logger.LogInformation($"DELETE request to /api/Category/{categoryId} returned status: {deleteResponse.StatusCode}");

            deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // Log category deletion
            _logger.LogInformation($"Category with ID {categoryId} deleted successfully");
        }
    }
