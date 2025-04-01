using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using To_Do_List.Core.Domain.Entities;
using Task = To_Do_List.Core.Domain.Entities.Task;

namespace To_Do_List.Infrastructure.Persistence.Context.EntitiesConfiguration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasOne(t => t.User)
                   .WithMany(u => u.Tasks)
                   .HasForeignKey(t => t.UserId);


            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.State).IsRequired();
            builder.Property(t => t.State)
                   .HasDefaultValue(States.Pendiente);


        }
    }
}
