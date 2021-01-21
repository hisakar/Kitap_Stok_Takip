using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }
        public string BookName { get; set; }
        public string Writer { get; set; }
        public int Price { get; set; }
        public int StockAmount { get; set; }
        public string Barcode { get; set; }
        public string AddedDate { get; set; }
    }
}
