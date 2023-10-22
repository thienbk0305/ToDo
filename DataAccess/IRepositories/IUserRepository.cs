using DataAccess.Entities;

namespace DataAccess.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUser(CancellationToken cancellation);
        Task<User> GetUser(int id, CancellationToken cancellation);
        Task<int> AddUser(User model, CancellationToken cancellation);
        Task UpdateUser(int id, User model, CancellationToken cancellation);
        Task DeleteUser(int id, CancellationToken cancellation);
    }
}
