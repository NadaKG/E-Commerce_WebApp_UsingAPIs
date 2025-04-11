using Application.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IAuthenticationService
    {
        Task<Authentication> RegisterAsync(RegisterDto model);
        Task<int> GetCountOfUsers();
        Task<Authentication> LoginAsync(LoginDto model);

    }
}
