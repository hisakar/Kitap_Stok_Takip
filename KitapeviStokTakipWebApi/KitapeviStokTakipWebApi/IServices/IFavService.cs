using KitapeviStokTakipWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.IServices
{
    public interface IFavService
    {
        IEnumerable<Fav> GetAllFavs();
        Fav GetFavByUserId(int userId);
        bool AddFav(Fav fav);
        void DeleteSingleFav(Fav fav);
        void DeleteFavsByUserId(int userId);

        IEnumerable<Book> GetAllBooksByUserId(int userId);
    }
}
