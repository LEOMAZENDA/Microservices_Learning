using GreekShoping.Web.Models;

namespace GreekShoping.Web.Services._ProductServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAllProducts(string token);
        Task<ProductViewModel> FindAllProductById(long id, string token);
        Task<ProductViewModel> CreateProduct(ProductViewModel model, string token);
        Task<ProductViewModel> UpdateProduct(ProductViewModel model, string token);
        Task<bool> DeleteProductById(long id, string token);
    }
}
