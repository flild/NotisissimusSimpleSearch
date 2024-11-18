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


namespace NotisissimusSimpleSearch.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IndexApi _indexApi;
        private readonly SearchApi _searchApi;
        private readonly UtilsApi _utilsApi;

        public ProductService(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
            var client = _httpClientFactory.CreateClient("Product");
            var config = new Configuration();
            config.BasePath = "http://127.0.0.1:9308";
            var httpClientHandler = new HttpClientHandler();
            // Perform insert and search operations
            _indexApi = new IndexApi(client, config, httpClientHandler);
            _searchApi = new SearchApi(client, config, httpClientHandler);
            _utilsApi = new UtilsApi(client, config, httpClientHandler);
        }
        public Product GetProductById(int id)
        {

            return new Product();
        }
        public void CreateProduct(Product product)
        {
            string tableName = "Products";
            try
            {
                Dictionary<string, Object> doc = new Dictionary<string, Object>();
                doc.Add("Name", product.Name);
                doc.Add("Description", product.Description);
                InsertDocumentRequest insertDocumentRequest = new InsertDocumentRequest(Index: "products", Doc: doc);
                _indexApi.Insert(insertDocumentRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public async Task<List<string>> GetProductViaFTSAsync(string request)
        {
            SearchRequest searchRequest = new SearchRequest(Index: "Products");
            Highlight queryHighlight = new Highlight();
            List<string> highlightFields = new List<string>();
            highlightFields.Add("Name");
            highlightFields.Add("Description");
            queryHighlight.Fields = highlightFields;
            SearchQuery query = new SearchQuery();
            query.Match = request;
            searchRequest.Query = query;
            searchRequest.Highlight = queryHighlight;

            SearchResponse searchResponse = _searchApi.Search(searchRequest);
            Console.WriteLine(searchResponse);
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
