using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface IUserService
    {        Task<string> LoginAsync(LoginRequest loginRequest);
        Task<string> SignUpAsync(User user);
    }
}
