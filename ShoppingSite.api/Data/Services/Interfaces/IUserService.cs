using ShoppingSite.api.Data.DataModels.Entity;

namespace ShoppingSite.api.Data.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task<(IEnumerable<UserEntity>, PaginationMetaData)> GetAllUsersAsync
            (string? name, bool? isAdmin, string? searchQuery, int pageNumber, int pageSize);
        Task<UserEntity?> GetUserByIdAsync(int id);
        Task AddUserAsync(UserEntity userEntity);
        Task DeleteUserAsync(int userId);
        Task<bool> IsUserExistsAsync(int userId);
        Task<bool> SaveChangesAsync();
    }
}
