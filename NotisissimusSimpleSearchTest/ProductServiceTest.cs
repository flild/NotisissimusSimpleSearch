using Microsoft.EntityFrameworkCore;
using NotisissimusSimpleSearch.Data;
using NotisissimusSimpleSearch.Models;
using NotisissimusSimpleSearch.Services;
using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotisissimusSimpleSearchTest
{
    public class ProductServiceTest
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var _db = new ApplicationDbContext(options);
            _db.Database.EnsureCreated();
            if (await _db.Products.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    _db.Products.Add(
                    new Product()
                    {
                        Name = "Перчатки",
                        Description = "Лучшие в мире перчатки из латекса"
                    });
                    await _db.SaveChangesAsync();
                }
            }
            return _db;
        }
        public ProductServiceTest() 
        {
            
        }
        [Fact]
        public async Task GetProductById_ReturnProduct()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var productService = new ProductService(dbContext);

            //Act
            var result = productService.GetProductById(1);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Product>();
        }

        [Fact]
        public async Task GenerateRandomData_ReturnVoid()
        {
            //Arrange
            var count = 100;
            var dbContext = await GetDatabaseContext();
            var productService = new ProductService(dbContext);

            //Act
            productService.GenerateRandomData(count);
            var result = dbContext.Products.Count();

            //Assert
            result.Should().BeGreaterThan(count);
        }

    }
}
