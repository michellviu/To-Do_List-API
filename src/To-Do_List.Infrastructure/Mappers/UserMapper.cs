using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.DTOs;
using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Services;

namespace To_Do_List.Infrastructure.Persistence.Mappers
{
    public class UserMapper
    {
        private IUserService _userService;
        public UserMapper(IUserService userService)
        {
            _userService = userService;
        }
        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                EmailAddress = user.Email,
                
            };
        }

        public async Task<User> ToEntity(UserDto userDto)
        {
            var user = await _userService.GetByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new Exception("Entity not found");
            }
            return user;
        }
    }
}
