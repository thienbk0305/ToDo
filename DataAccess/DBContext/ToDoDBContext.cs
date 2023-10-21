using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBContext
{
    public class ToDoDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ToDoDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder) { base.OnModelCreating(builder); }
        public DbSet<User>? Users { get; set; }
        public DbSet<Todo>? Todos { get; set; }
    }
}
