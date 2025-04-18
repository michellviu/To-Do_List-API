﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace To_Do_List.Infrastructure.Services.AppService;

public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration configuration;
    private readonly UserManager<User> userManager;
    public TokenGenerator(IConfiguration configuration, UserManager<User> userManager)
    {
        this.configuration = configuration;
        this.userManager = userManager;
    }
    public string GenerateJwtTokenAsync(User user)
    {
        //var userRoles = await userManager.GetRolesAsync(user);
        var claims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", user.Id.ToString())
            };

        //claims.AddRange(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SigningKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

