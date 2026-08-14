using App01.Core.Entities;

namespace App01.Core.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product? GetById(int productId);
        List<Product> GetByCategory(int categoryId);
        int Insert(Product product);
        bool UpdatePrice(int productId, decimal newPrice);
        bool Delete(int productId);
    }
}
