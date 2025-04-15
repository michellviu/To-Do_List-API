using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Core.Domain.Entities;

public class TodoItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public int? Difficulty { get; set; }
    public required int UserId { get; set; }
    public User User { get; set; }

}
public enum Status
{
    Pendiente,
    Realizada,
    Cancelada
}

