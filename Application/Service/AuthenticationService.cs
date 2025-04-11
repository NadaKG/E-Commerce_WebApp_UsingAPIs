using Application.DTOs;
using Application.Service_Interface;
using BCrypt.Net;
using Domain.Model;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context, IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _context = context;
        }

        public async Task<Authentication> RegisterAsync(RegisterDto model)
        {
            var existingUsers = await _userService.GetAllAsync();
            if (existingUsers.Any(u => u.Email == model.Email))
            {
                return new Authentication { IsAuthenticated = false, Message = "Email already exists." };
            }
            if (existingUsers.Any(u => u.UserName == model.UserName))
            {
                return new Authentication { IsAuthenticated = false, Message = "Username already exists." };
            }
            var userRole = "Customer";

            var user = new User
            {
                Email = model.Email,
                Password = HashPassword(model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Role = userRole
            };
            user.CreatedAt = DateTime.UtcNow;

            await _userService.Add(user);

            var token = GenerateJwtToken(user);

            return new Authentication
            {
                IsAuthenticated = true,
                Message = "Registration successful.",
                Username = user.UserName,
                Email = user.Email,
                Token = token,
                Roles = new List<string> { user.Role },
                ExpiresOn = DateTime.UtcNow.AddDays(30)
            };
        }


        private string HashPassword(string password)
        {

            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:DurationInDays"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<int> GetCountOfUsers()
        {
            return await _context.Users.CountAsync();

        }

        public async Task<Authentication> LoginAsync(LoginDto model)
        {
            var user = await _userService.FindByEmailAsync(model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return new Authentication { IsAuthenticated = false, Message = "Invalid credentials" };
            }
            user.LoginDate = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            var token = GenerateJwtToken(user);

            return new Authentication
            {
                IsAuthenticated = true,
                Message = "Login successful.",
                Username = user.UserName,
                Email = user.Email,
                Token = token,
                Roles = new List<string> { user.Role },
                ExpiresOn = DateTime.UtcNow.AddDays(30)
            };
        }

    }
}
