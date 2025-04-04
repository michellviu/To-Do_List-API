using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;

namespace To_Do_List.Core.Domain.DTOs;

public class TodoItemDto
{
    public int Id { get; init; }
    [Required]
    public string Title { get; init; }
    public string Description { get; init; }
    public States State { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime LastUpdatedDate { get; init; }
}

