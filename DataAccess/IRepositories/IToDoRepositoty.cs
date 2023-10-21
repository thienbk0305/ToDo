using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IToDoRepositoty
    {
        Task<List<Todo>> GetAllTodo();
        Task<Todo> GetTodo(int id);
        Task<Todo> GetTodoById(int id);
        Task<int> AddTodo(Todo model);
        Task UpdateTodo(int id, Todo model);
        Task DeleteTodo(int id);
    }
}
