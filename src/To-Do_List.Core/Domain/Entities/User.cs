﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace To_Do_List.Core.Domain.Entities;

public class User : IdentityUser<int>
{
    public ICollection<TodoItem> TodoItems { get; set; }
}

