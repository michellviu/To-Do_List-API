using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using To_Do_List.Core.Domain.Entities;

namespace To_Do_List.Infrastructure.Persistence.Context.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.TodoItems)
                   .WithOne(t => t.User)
                   .HasForeignKey(t => t.UserId);
        }
    }
}
