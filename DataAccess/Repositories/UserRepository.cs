using Microsoft.EntityFrameworkCore;

using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ToDoDBContext _dbContext;
        public UserRepository(ToDoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddUserAsync(User model, CancellationToken cancellation)
        {
            try
            {
                await _dbContext.Users!.AddAsync(model, cancellation);
                await _dbContext.SaveChangesAsync(cancellation);
                return model.ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Task DeleteUserAsync(int id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
        public async Task<List<User>> GetAllUserAsync(CancellationToken cancellation)
        {
            var todos = await _dbContext.Users!.ToListAsync(cancellation);
            return todos;
        }
        public async Task<User> GetUserAsync(int id, CancellationToken cancellation)
        {
            try
            {
                var users = await _dbContext.Users!.FindAsync(id, cancellation);
                return users!;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Task UpdateUserAsync(int id, User model, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
