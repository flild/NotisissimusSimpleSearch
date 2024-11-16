using NotisissimusSimpleSearch.Models;

namespace NotisissimusSimpleSearch.Services
{
    public interface IProductService
    {
        public Task<List<string>>  GetProductViaFTSAsync(string query);
        public void GenerateRandomData(int count);

    }
}
