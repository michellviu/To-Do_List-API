using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using To_Do_List.Core.Domain.Entities;

namespace To_Do_List.Infrastructure.Persistence.Context.EntitiesConfiguration;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasOne(t => t.User)
               .WithMany(u => u.TodoItems)
               .HasForeignKey(t => t.UserId);


        builder.Property(t => t.Title).IsRequired();
        builder.Property(t => t.State).IsRequired();
        builder.Property(t => t.State)
               .HasDefaultValue(States.Pendiente);


    }
}

