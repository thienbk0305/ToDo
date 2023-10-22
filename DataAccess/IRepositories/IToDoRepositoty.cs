using DataAccess.Entities;

namespace DataAccess.IRepositories
{
    public interface IToDoRepositoty
    {
        Task<List<Todo>> GetAllTodoAsync(CancellationToken cancellation);
        Task<Todo> GetTodoAsync(int id, CancellationToken cancellation);
        Task<Todo> GetTodoByIdAsync(int id, CancellationToken cancellation);
        Task<int> AddTodoAsync(Todo model, CancellationToken cancellation);
        Task UpdateTodoAsync(int id, Todo model, CancellationToken cancellation);
        Task DeleteTodoAsync(int id, CancellationToken cancellation);
    }
}
