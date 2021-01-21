using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Models
{
    public class CategoryLog
    {
        public int LogId { get; set; }
        public string LogType { get; set; }
        public string NewName { get; set; }
        public string OldName { get; set; }
        public string LogDate { get; set; }
    }
}
