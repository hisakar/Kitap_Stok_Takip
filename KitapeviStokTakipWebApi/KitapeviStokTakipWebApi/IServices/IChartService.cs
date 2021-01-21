using KitapeviStokTakipWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.IServices
{
    public interface IChartService
    {
        IEnumerable<Chart> GetAllChartItems();
        Chart GetChartItemByUserId(int userId);
        bool AddChartItem(Chart chartItem);
        void DeleteSingleChartItem(Chart chartItem);
        void DeleteChartItemByUserId(int userId);

        IEnumerable<Book> GetAllBooksByUserId(int userId);
    }
}
