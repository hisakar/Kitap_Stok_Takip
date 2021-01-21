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
    public class FavsController : ControllerBase
    {
        IFavService favService;
        public FavsController(IFavService _favService)
        {
            favService = _favService;
        }

        [Authorize]
        [HttpPost]
        public bool AddFav([FromBody] Fav fav)
        {
            return favService.AddFav(fav); // added return true otherwise there is a fav in table so return false 
        }

        [HttpGet("{userId}")]
        public IEnumerable<Book> GetBooksByUserId(int userId)
        {
            return favService.GetAllBooksByUserId(userId);
        }

        [HttpDelete]
        public void DeleteSingleFavorite([FromBody] Fav fav)
        {
            favService.DeleteSingleFav(fav);
        }

    }
}