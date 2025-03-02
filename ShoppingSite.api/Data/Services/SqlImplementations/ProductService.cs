using Microsoft.EntityFrameworkCore;
using ShoppingSite.api.Data.DataModels.Entity;
using ShoppingSite.api.Data.DataModels.Model;
using ShoppingSite.api.Data.Services.Interfaces;

namespace ShoppingSite.api.Data.Services.SqlImplementations
{
    public class ProductService : IProductService
    {
        private readonly DataContext db;

        public ProductService(DataContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
        {
            return await db.Products.OrderBy(product => product.Name).ToListAsync();
        }

        public async Task<(IEnumerable<ProductEntity>, PaginationMetaData)> GetAllProductsAsync
            (string? searchQuery, int pageNumber, int pageSize)
        {
            IQueryable<ProductEntity> collection = db.Products as IQueryable<ProductEntity>;
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                collection = collection.Where(product => product.Name.Contains(searchQuery));
            }

            int totalProductsCount = await collection.CountAsync();
            PaginationMetaData paginationMetaData = new PaginationMetaData(totalProductsCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(product => product.Name)
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task<ProductEntity?> GetProductByIdAsync(int id)
        {
            return await db.Products.FindAsync(id);
        }

        public async Task<bool> IsProductExistsAsync(int id)
        {
            ProductEntity? productEntity = await GetProductByIdAsync(id);
            return productEntity != null;
        }

        public async Task AddProductAsync(ProductEntity product)
        {
            await db.Products.AddAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            ProductEntity? productEntity = await GetProductByIdAsync(id);
            if(productEntity != null)
            {
                db.Products.Remove(productEntity);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await db.SaveChangesAsync() > 0;
        }
    }
}
