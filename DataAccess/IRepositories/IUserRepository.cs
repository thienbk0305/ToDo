using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUser();
        Task<User> GetUser(int id);
        Task<int> AddUser(User model);
        Task UpdateUser(int id, User model);
        Task DeleteUser(int id);
    }
}
