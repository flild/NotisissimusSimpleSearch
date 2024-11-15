using NotisissimusSimpleSearch.Models;

namespace NotisissimusSimpleSearch.Services
{
    public interface IProductService
    {
        public List<string>  GetProductViaFTSAsync(string query);
        public void GenerateRandomData(int count);

    }
}
