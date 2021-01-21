using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Models
{
    public class Chart
    {
        public User User { get; set; }
        public Book Book { get; set; }
        public string OrderedDate { get; set; }
    }
}
