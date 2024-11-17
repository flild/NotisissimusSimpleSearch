using Microsoft.EntityFrameworkCore;
using NotisissimusSimpleSearch.Data;
using NotisissimusSimpleSearch.Extension;
using NotisissimusSimpleSearch.Models;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NotisissimusSimpleSearch.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db) 
        {
            _db = db;
        }
        public Product GetProductById(int id)
        {
            return _db.Products.SingleOrDefault(p => p.Id == id);
        }
        public async Task<List<string>> GetProductViaFTSAsync(string query)
        {
            var sql = "SELECT * FROM \"Products\" " +
                "WHERE to_tsvector('russian', coalesce(\"Name\", '') || ' ' || " +
                $"coalesce(\"Description\", '')) @@ to_tsquery('russian', '{query}' || ':*')";
            var products = _db.Products
                .FromSqlRaw(sql)
                .Select(p => p.Name)
                .Take(10)
                .ToList();
            return products;
        }
        public void GenerateRandomData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var product = new Product
                {
                    Name = RandomDataGenerator.GenerateRandomString(7),
                    Description = RandomDataGenerator.GenerateRandomString(20)
                };

                _db.Products.Add(product);
                if(i %2000 == 0)
                {
                    _db.SaveChanges();
                }
            }

            _db.SaveChanges();
        }


    }
}
