using GreekShoping.ProductApi.Data.ValueObjects;

namespace GreekShoping.ProductApi.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductVO>> FindAll();
    Task<ProductVO> FindById(long id);
    Task<ProductVO> Create(ProductVO vO);
    Task<ProductVO> Update(ProductVO vO);
    Task<bool> Delete(long id);
}
