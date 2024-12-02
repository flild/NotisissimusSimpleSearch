using Azure.Core;
using Microsoft.EntityFrameworkCore;
using NotisissimusSimpleSearch.Data;
using NotisissimusSimpleSearch.Extension;
using NotisissimusSimpleSearch.Models;
using ManticoreSearch.Api;
using ManticoreSearch.Client;
using ManticoreSearch.Model;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Http;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Text;


namespace NotisissimusSimpleSearch.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IndexApi _indexApi;
        private readonly SearchApi _searchApi;
        private readonly UtilsApi _utilsApi;
        private readonly HttpClient client;
        private readonly string BasePath = "http://127.0.0.1:9306";
        private readonly HttpClient _manticoreClient;

        public ProductService(HttpClient manticoreClient) 
        {
            _manticoreClient = manticoreClient;
        }
        public Product GetProductById(int id)
        {

            return new Product();
        }
        public void CreateProduct(Product product)
        {
           

        }
        public async Task<List<string>> GetProductViaFTSAsync(string request)
        {
            // Формируем тело запроса
            var requestBody = new { query = "SELECT * FROM poisk" };

            // Отправляем POST-запрос
            HttpResponseMessage response = await _manticoreClient.PostAsJsonAsync("sql", requestBody);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                // Здесь вы можете обработать результат запроса, например, десериализовать JSON
                Console.WriteLine(result);// Пример десериализации

            }
            else
            {
                // Обработка ошибки
                Console.WriteLine($"Ошибка при выполнении запроса: {(int)response.StatusCode} {response.ReasonPhrase}");
            }

            return new List<string>();
        }
        public void GenerateRandomData(int count)
        {

        //---
            for (int i = 0; i < count; i++)
            {

            }
           

            _db.SaveChanges();
        }

    }
}
