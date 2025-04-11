using Application.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        User Update(User user);
        Task<User> Add(User user);
        User Delete(User user);
        Task<User> GetById(int id);
        Task<User> FindByEmailAsync(string email);
        public Task<bool> UpdateUserAddressAsync(int userId, CheckOutDTO addressDetails);
        public Task<UserProfileDTO> GetUserProfileAsync(int userId);
        public Task<bool> UpdateUserProfileAsync(int userId, UserProfileDTO userProfileDto);
        public Task<List<AllUsersDto>> GetAllUserDetailsAsync();


    }
}
