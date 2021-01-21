using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Models
{
    public class Fav
    {
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
