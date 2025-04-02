using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Infrastructure.Persistence.Context.EntitiesConfiguration;

namespace To_Do_List.Infrastructure.Persistence.Context
{
    public class AppDBContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TodoItemConfiguration());

        }

    }
}
