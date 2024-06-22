using GreekShoping.Web.Models;

namespace GreekShoping.Web.Services.IServices._ProductIServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAllProducts(string token);
        Task<ProductModel> FindAllProductById(long id, string token);
        Task<ProductModel> CreateProduct(ProductModel model, string token);
        Task<ProductModel> UpdateProduct(ProductModel model, string token);
        Task<bool> DeleteProductById(long id, string token);
    }
}
