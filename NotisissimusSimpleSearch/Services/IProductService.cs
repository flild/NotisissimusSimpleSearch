using NotisissimusSimpleSearch.Models;

namespace NotisissimusSimpleSearch.Services
{
    public interface IProductService
    {
        public Product GetProductById(int productId);
        public Task<List<string>>  GetProductViaFTSAsync(string query);
        public void GenerateRandomData(int count);
        public void CreateProduct(Product product);

    }
}
