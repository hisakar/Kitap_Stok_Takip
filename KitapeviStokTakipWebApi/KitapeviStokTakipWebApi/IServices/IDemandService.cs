using KitapeviStokTakipWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.IServices
{
    public interface IDemandService
    {
        IEnumerable<Demand> GetAllDemands();
        Demand GetDemandByUserId(int userId);
        Demand AddDemand(Demand demand);
        void DeleteSingleDemand(Demand demand);
        void DeleteDemandsByUserId(int userId);

        IEnumerable<Demand> GetAllDemandsByUserId(int userId);
    }
}
