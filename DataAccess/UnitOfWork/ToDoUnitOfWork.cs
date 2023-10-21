using DataAccess.DBContext;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class ToDoUnitOfWork : IToDoUnitOfWork
    {
        public IToDoRepositoty ToDoRepositoty { get; }
        public IUserRepository UserRepositoty { get; }

        private ToDoDBContext _toDoDBContext;
        public ToDoUnitOfWork (ToDoDBContext toDoDBContext, IToDoRepositoty toDoRepositoty, IUserRepository userRepository)
        {
                ToDoRepositoty = toDoRepositoty;
                _toDoDBContext = toDoDBContext;
                UserRepositoty = userRepository;
        }

        public int Save()
        {
            return _toDoDBContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toDoDBContext.Dispose();
            }
        }
    }
}
