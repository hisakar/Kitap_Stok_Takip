using KitapeviStokTakipWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.IServices
{
    public interface IAuthService
    {
        IEnumerable<User> GetAllUsers();
        string RegisterUser(User user);
        Task<User> Authenticate(string username, string password);
    }
}
