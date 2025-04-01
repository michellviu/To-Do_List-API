using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;

namespace To_Do_List.Core.DomainService.Services
{
    public interface ITokenGenerator
    {
        string GenerateJwtTokenAsync(User user);
    }
}
