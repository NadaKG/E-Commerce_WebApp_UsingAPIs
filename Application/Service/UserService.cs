using Application.DTOs;
using Application.Service_Interface;
using Domain.Model;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);

            _context.SaveChanges();
            return user;
        }

        public User Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public User Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<bool> UpdateUserAddressAsync(int userId, CheckOutDTO addressDetails)
        {
            var address = await _context.User_Addresses.FirstOrDefaultAsync(ua => ua.UserId == userId);

            if (address == null)
            {
                address = new User_Address
                {
                    UserId = userId,
                    Address = addressDetails.Address,
                    City = addressDetails.City,
                    PostalCode = addressDetails.PostalCode,
                    Country = addressDetails.Country,
                    PhoneNumber = addressDetails.PhoneNumber
                };
                _context.User_Addresses.Add(address);
            }
            else
            {
                address.Address = addressDetails.Address;
                address.City = addressDetails.City;
                address.PostalCode = addressDetails.PostalCode;
                address.Country = addressDetails.Country;
                address.PhoneNumber = addressDetails.PhoneNumber;
                _context.User_Addresses.Update(address);
            }

            await _context.SaveChangesAsync();

            var user = await _context.Users.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                if (user.Addresses == null)
                {
                    user.Addresses = new List<User_Address>();
                }

                if (!user.Addresses.Any(a => a.Id == address.Id))
                {
                    user.Addresses.Add(address);
                }

                await _context.SaveChangesAsync();
            }

            return true;
        }



        public async Task<UserProfileDTO> GetUserProfileAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            return new UserProfileDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Addresses.FirstOrDefault()?.Address,
                City = user.Addresses.FirstOrDefault()?.City,
                PostalCode = user.Addresses.FirstOrDefault()?.PostalCode,
                Country = user.Addresses.FirstOrDefault()?.Country,
                PhoneNumber = user.Addresses.FirstOrDefault()?.PhoneNumber
            };
        }

        public async Task<bool> UpdateUserProfileAsync(int userId, UserProfileDTO userProfileDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            // Check if the new username or email already exists
            if (_context.Users.Any(u => u.Id != userId && (u.UserName == userProfileDto.UserName || u.Email == userProfileDto.Email)))
            {
                throw new Exception("Username or Email already exists.");
            }

            // Update basic information
            user.FirstName = userProfileDto.FirstName;
            user.LastName = userProfileDto.LastName;
            user.UserName = userProfileDto.UserName;
            user.Email = userProfileDto.Email;

            // Update address information
            if (user.Addresses == null)
            {
                user.Addresses = new List<User_Address>(); // Initialize the Addresses collection if it's null
            }

            var address = user.Addresses.FirstOrDefault();
            if (address != null)
            {
                // Update existing address
                address.Address = userProfileDto.Address;
                address.City = userProfileDto.City;
                address.PostalCode = userProfileDto.PostalCode;
                address.Country = userProfileDto.Country;
                address.PhoneNumber = userProfileDto.PhoneNumber;
            }
            else
            {
                // Add new address if it doesn't exist
                user.Addresses.Add(new User_Address
                {
                    Address = userProfileDto.Address,
                    City = userProfileDto.City,
                    PostalCode = userProfileDto.PostalCode,
                    Country = userProfileDto.Country,
                    PhoneNumber = userProfileDto.PhoneNumber,
                    UserId = userId
                });
            }

            // Update password if provided and matches confirmation
            if (!string.IsNullOrEmpty(userProfileDto.NewPassword))
            {
                if (userProfileDto.NewPassword != userProfileDto.ConfirmPassword)
                {
                    throw new ArgumentException("New Password and Confirm Password do not match.");
                }
                user.Password = HashPassword(userProfileDto.NewPassword);  // Make sure to hash the new password
            }
            user.UpdatedAt = DateTime.UtcNow;

            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<AllUsersDto>> GetAllUserDetailsAsync()
        {
            var users = await _context.Users.Select(u => new AllUsersDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                LoginDate = u.LoginDate,
                UpdatedAt = u.UpdatedAt,
                Role = u.Role,
            }).ToListAsync();

            return users;
        }


    }
}