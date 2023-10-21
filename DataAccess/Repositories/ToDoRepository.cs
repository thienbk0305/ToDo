using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DataAccess.Repositories
{
   
    public class ToDoRepository : IToDoRepositoty
    {
        private ToDoDBContext _dbContext;
        public ToDoRepository(ToDoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Adnew ToDo
        public async Task<int> AddTodo(Todo model)
        {
            try
            {
                _dbContext.Todos!.Add(model);
                await _dbContext.SaveChangesAsync();
                return model.ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete ToDo
        public async Task DeleteTodo(int id)
        {
            try
            {
                var deleteTodo = _dbContext.Todos!.SingleOrDefault(t => t.ID == id);
                if (deleteTodo != null)
                {
                    _dbContext.Todos!.Remove(deleteTodo);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        //Show All ToDo
        public async Task<List<Todo>> GetAllTodo()
        {
            //var todos = await _dbContext.Todos!.ToListAsync();
            var todos = from t in _dbContext.Todos
                        join u in _dbContext.Users! on t.UserId equals u.ID
                        orderby u.ID
                        select new DataAccess.Entities.Todo
                        {
                            ID = t.ID,
                            UserId = t.UserId,
                            UserName = u,
                            Title = t.Title,
                            Content = t.Content,
                            ShortDesc = t.ShortDesc,
                            AppointmentDate = t.AppointmentDate,
                            Status = t.Status,
                        };
            var result = await todos.ToListAsync();
            return result;
        }
        //Show ToDo By Id
        public async Task<Todo> GetTodo(int id)
        {
            try
            {
                var todos = await _dbContext.Todos!.FindAsync(id);
                return todos!;
            }
            catch (Exception)
            {

                throw;
            }

        }
        //Show ToDo
        public async Task<Todo> GetTodoById(int id)
        {
            try
            {
                var todos = await _dbContext.Todos!.AsNoTracking().Include(c => c.UserName).FirstOrDefaultAsync(c => c.ID==id);
                return todos!;
            }
            catch (Exception)
            {

                throw;
            }

        }
        //Update ToDo
        public async Task UpdateTodo(int id, Todo model)
        {
            try
            {
                if (id == model.ID)
                {
                    _dbContext.Todos!.Update(model);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
