using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ToDoDBContext _dbContext;
        public UserRepository(ToDoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddUser(User model)
        {
            try
            {
                _dbContext.Users!.Add(model);
                await _dbContext.SaveChangesAsync();
                return model.ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<User>> GetAllUser()
        {
            var todos = await _dbContext.Users!.ToListAsync();
            return todos;
        }
        public async Task<User> GetUser(int id)
        {
            try
            {
                var users = await _dbContext.Users!.FindAsync(id);
                return users!;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Task UpdateUser(int id, User model)
        {
            throw new NotImplementedException();
        }
    }
}
