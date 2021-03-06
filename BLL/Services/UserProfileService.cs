﻿using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public  class UserProfileService : IUserProfileService
    {
        private readonly SignInManager<UserProfile> signInManager;
        private readonly UserManager<UserProfile> userManager;
        private readonly IConfiguration configuration;

        public UserProfileService(UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<object> Register(RegisterDTO data)
        {
            var user = new UserProfile
            {
                UserName = data.UserName,
                Email = data.UserEmail
            };

            var result = await userManager.CreateAsync(user, data.UserPass);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return GenerateJwtToken(data.UserEmail, user);
            }
            else
            {
                string errorInfo = "";
                foreach (var error in result.Errors)
                {
                    errorInfo += error.Description + "\n";
                }
                throw new ApplicationException(errorInfo);
            }
        }

        public async Task<object> Login(LoginDTO data)
        {
            var user = userManager.Users.SingleOrDefault(u => u.Email == data.Email);

            var result = await signInManager.PasswordSignInAsync(user, data.Password, false, false);
            
            if (result.Succeeded)
            {
                return GenerateJwtToken(data.Email, user);
            }
            else
            {
                throw new ApplicationException("login error");
            }
        }
    }
}
