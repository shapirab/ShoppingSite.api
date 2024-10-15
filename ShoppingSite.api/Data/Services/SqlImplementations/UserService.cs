using Microsoft.EntityFrameworkCore;
using ShoppingSite.api.Data.DataModels.Entity;
using ShoppingSite.api.Data.Services.Interfaces;

namespace ShoppingSite.api.Data.Services.SqlImplementations
{
    public class UserService : IUserService
    {
        private readonly DataContext db;

        public UserService(DataContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await db.Users.OrderBy(user => user.Name).ToListAsync();
        }

        public async Task<(IEnumerable<UserEntity>, PaginationMetaData)> GetAllUsersAsync
            (string? name, bool? isAdmin, string? searchQuery, int pageNumber, int pageSize)
        {
            IQueryable<UserEntity> collection = db.Users as IQueryable<UserEntity>;

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(user => user.Name == name);
            }

            if(isAdmin != null)
            {
                collection = collection.Where(user => user.IsAdmin == isAdmin);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                collection = collection.Where(user => user.Name.Contains(searchQuery));
            }

            int totalItemCount = await collection.CountAsync();

            PaginationMetaData paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageSize);

            var collectionToReturn = await collection.OrderBy(user => user.Name)
                .Skip(pageSize * pageNumber - 1)
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task<UserEntity?> GetUserByIdAsync(int id)
        {
            return await db.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(UserEntity userEntity)
        {
            await db.Users.AddAsync(userEntity);
        }

        public async Task DeleteUserAsync(int userId)
        {
           UserEntity? userEntity = await GetUserByIdAsync(userId);
            if (userEntity != null)
            {
                db.Users.Remove(userEntity);
            }
        }

        public async Task<bool> IsUserExistsAsync(int userId)
        {
            return await GetUserByIdAsync(userId) != null;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await db.SaveChangesAsync() >= 0;
        }
    }
}
