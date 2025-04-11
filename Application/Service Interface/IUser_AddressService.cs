using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IUser_AddressService
    {
        Task<IEnumerable<User_Address>> GetAllAsync();
        User_Address Update(User_Address user);
        Task<User_Address> Add(User_Address user);
        User_Address Delete(User_Address user);
        Task<User_Address> GetById(int id);
    }
}
