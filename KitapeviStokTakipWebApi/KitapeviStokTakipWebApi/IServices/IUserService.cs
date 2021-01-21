using KitapeviStokTakipWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.IServices
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers ();
        User GetUserById(int id);
        int DeleteUser(int id);
    }
}
