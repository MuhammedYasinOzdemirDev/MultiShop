using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Mongo2Go;
using MongoDB.Driver;
using MultiShop.Catalog.Settings;
using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace MultiShop.Catalog.Tests.IntegrationTests;

  public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private MongoDbRunner _runner;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Start MongoDB in-memory runner
                _runner = MongoDbRunner.Start();

                // Replace the IDatabaseSettings service with a test implementation
                var databaseSettings = new DatabaseSettings
                {
                    ConnectionString = "mongodb+srv://yasin:12345@multishop-catalog.dzxvptc.mongodb.net/?retryWrites=true&w=majority&appName=MultiShop-Catalog",
                    DatabaseName = "IntegrationTestDatabase",
                    CategoryCollectionName = "Categories",
                    ProductCollectionName = "Products",
                    ProductDetailCollectionName = "ProductDetails",
                    ProductImageCollectionName = "ProductImages"
                };

                services.Replace(new ServiceDescriptor(typeof(IDatabaseSettings), databaseSettings));

                // Register IMongoClient with the provided connection string
                services.AddSingleton<IMongoClient>(s =>
                {
                    return new MongoClient(databaseSettings.ConnectionString);
                });

                // Log the configuration
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                logger.LogInformation($"Configured database settings: {databaseSettings.ConnectionString}, {databaseSettings.DatabaseName}");

                // Create a scope to obtain a reference to the database context (AppDbContext)
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    try
                    {
                        var client = scopedServices.GetRequiredService<IMongoClient>();
                        var database = client.GetDatabase(databaseSettings.DatabaseName);

                        // Ensure the database is clean before starting tests
                        database.DropCollection(databaseSettings.CategoryCollectionName);
                        database.DropCollection(databaseSettings.ProductCollectionName);
                        database.DropCollection(databaseSettings.ProductDetailCollectionName);
                        database.DropCollection(databaseSettings.ProductImageCollectionName);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the database with test data. Error: {Message}", ex.Message);
                    }
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _runner?.Dispose();
        }
    }