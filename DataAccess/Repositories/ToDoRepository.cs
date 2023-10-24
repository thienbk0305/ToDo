using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;

using Microsoft.EntityFrameworkCore;

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
        public async Task<int> AddTodoAsync(Todo model, CancellationToken cancellation)
        {
            try
            {
                await _dbContext.Todos!.AddAsync(model, cancellation);
                await _dbContext.SaveChangesAsync(cancellation);
                return model.ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete ToDo
        public async Task DeleteTodoAsync(int id, CancellationToken cancellation)
        {
            try
            {
                var deleteTodo = await _dbContext.Todos!.SingleOrDefaultAsync(t => t.ID == id, cancellation);
                if (deleteTodo != null)
                {
                    _dbContext.Todos!.Remove(deleteTodo);
                    await _dbContext.SaveChangesAsync(cancellation);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        //Show All ToDo
        public async Task<List<Todo>> GetAllTodoAsync(CancellationToken cancellation)
        {
            //var todos = await _dbContext.Todos!.ToListAsync();
            var todos = from t in _dbContext.Todos
                        join u in _dbContext.Users! on t.UserId equals u.ID
                        orderby u.ID
                        select new Todo
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
            var result = await todos.ToListAsync(cancellation);
            return result;
        }
        //Show ToDo By Id
        public async Task<Todo> GetTodoAsync(int id, CancellationToken cancellation)
        {
            try
            {
                var todos = await _dbContext.Todos!.FindAsync(id, cancellation);
                return todos!;
            }
            catch (Exception)
            {

                throw;
            }

        }
        //Show ToDo
        public async Task<Todo> GetTodoByIdAsync(int id, CancellationToken cancellation)
        {
            try
            {
                var todos = await _dbContext.Todos!.AsNoTracking().Include(c => c.UserName).FirstOrDefaultAsync(c => c.ID==id, cancellation);
                return todos!;
            }
            catch (Exception)
            {

                throw;
            }

        }
        //Update ToDo
        public async Task UpdateTodoAsync(int id, Todo model, CancellationToken cancellation)
        {
            try
            {
                if (id == model.ID)
                {
                    _dbContext.Todos!.Update(model);
                    await _dbContext.SaveChangesAsync(cancellation);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
