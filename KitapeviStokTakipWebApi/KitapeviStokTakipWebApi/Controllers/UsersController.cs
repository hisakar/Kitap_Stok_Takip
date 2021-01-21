using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitapeviStokTakipWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService userService;

        public UsersController(IUserService _userService)
        {
            this.userService = _userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            IEnumerable<User> users = userService.GetAllUsers();

            return users;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public User Get(int id)
        {
            User user = userService.GetUserById(id);

            return user;
        }

        //[Authorize(Roles = "Admin")]  //not used yet
        //[HttpDelete("{id}")]
        //public int Delete(int id)
        //{
        //     return userService.DeleteUser(id);
        //}

    }
}