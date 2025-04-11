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
    public class User_AddressService : IUser_AddressService
    {
        private readonly ApplicationDbContext _context;
        public User_AddressService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User_Address> Add(User_Address user)
        {
            await _context.User_Addresses.AddAsync(user);

            _context.SaveChanges();
            return user;
        }

        public User_Address Delete(User_Address user)
        {
            _context.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<IEnumerable<User_Address>> GetAllAsync()
        {
            return await _context.User_Addresses.ToListAsync();
        }

        public async Task<User_Address> GetById(int id)
        {
            return await _context.User_Addresses.SingleOrDefaultAsync(p => p.Id == id);
        }

        public User_Address Update(User_Address user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
