using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Models
{
    public class BookLog
    {
        public int LogId { get; set; }
        public string LogType { get; set; }
        public string NewName { get; set; }
        public string NewWriter { get; set; }
        public string NewPrice { get; set; }
        public string NewStockAmount { get; set; }
        public string NewBarcode { get; set; }
        public string NewCategoryId { get; set; }
        public string OldName { get; set; }
        public string OldWriter { get; set; }
        public string OldPrice { get; set; }
        public string OldStockAmount { get; set; }
        public string OldBarcode { get; set; }
        public string OldCategoryId { get; set; }
        public string LogDate { get; set; }
    }
}
