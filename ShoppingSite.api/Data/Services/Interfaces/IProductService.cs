using ShoppingSite.api.Data.DataModels.Entity;
using ShoppingSite.api.Data.DataModels.Model;

namespace ShoppingSite.api.Data.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductEntity>> GetAllProductsAsync();
        Task<(IEnumerable<ProductEntity>, PaginationMetaData)> GetAllProductsAsync
            (string? searchQuery, int pageNumber, int pageSize);
        Task<ProductEntity?> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductEntity product);
        Task DeleteProductAsync(int id);
        Task<bool> IsProductExistsAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
