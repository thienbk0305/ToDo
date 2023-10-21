using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IToDoUnitOfWork
    {
        IToDoRepositoty ToDoRepositoty { get; }
        IUserRepository UserRepositoty { get; }
        int Save();
    }
}
