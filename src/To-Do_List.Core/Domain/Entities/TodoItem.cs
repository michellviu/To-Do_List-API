﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Core.Domain.Entities;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public States State { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

}
public enum States
{
    Pendiente,
    Realizada,
    Cancelada
}

