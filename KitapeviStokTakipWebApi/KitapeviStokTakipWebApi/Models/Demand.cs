using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Models
{
    public class Demand
    {
        public int DemandId { get; set; }
        public User User { get; set; }
        public string BookName { get; set; }
        public string Writer { get; set; }
        public string RequestedDate { get; set; }
    }
}
