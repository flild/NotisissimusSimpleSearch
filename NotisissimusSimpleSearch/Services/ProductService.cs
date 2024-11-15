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
            }
            var product1 = new Product
            {
                Name = "Перчатки",
                Description = "Перчатки хозяйственные Aviora резиновые M "
            };
            var product2 = new Product
            {
                Name = "Гусь-обнимусь",
                Description = "Гусь Мягкая игрушка Гусь-обнимусь упаковывается вакуумным способом для более удобной и практичной транспортировки. После распаковки игрушки Гусь, наполнитель синтешар необходимо распределить равномерно внутри игрушки вручную и немного ее взбить, так как при откачке воздуха он спрессовывается. Не знаете что подарить на день рождение ребёнку? Или как просто удивить? Тогда вам нужна самая популярная игрушка этого год."
            };


            _db.SaveChanges();
        }

        public List<string> GetProductViaFTSAsync(string query)
        {

            var sql = $@"SELECT ""Name"" FROM ""Products""
             WHERE to_tsvector(""russian"", ""Name"" || ' ' || ""Description"") @@ {query}";

            var products = _db.Products
                .FromSqlRaw(sql)
                .Select(p => p.Name)
                .Take(10)
                .ToList();
            return products;
        }
    }
}
