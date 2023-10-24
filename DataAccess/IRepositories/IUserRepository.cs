using DataAccess.Entities;

namespace DataAccess.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUserAsync(CancellationToken cancellation);
        Task<User> GetUserAsync(int id, CancellationToken cancellation);
        Task<int> AddUserAsync(User model, CancellationToken cancellation);
        Task UpdateUserAsync(int id, User model, CancellationToken cancellation);
        Task DeleteUserAsync(int id, CancellationToken cancellation);
    }
}
