using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Core.Domain.DTOs;

public class UserDto
{
    public int Id { get; init; }
    [Required]
    public string UserName { get; init; }
    [Required]
    public string EmailAddress { get; init; }
}

