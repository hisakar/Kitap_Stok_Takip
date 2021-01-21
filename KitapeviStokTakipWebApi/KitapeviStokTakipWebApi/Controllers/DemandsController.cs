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
    public class DemandsController : ControllerBase
    {
        IDemandService demandService;
        public DemandsController(IDemandService _demandService)
        {
            this.demandService = _demandService;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IEnumerable<Demand> Get()
        {
            IEnumerable<Demand> demands = demandService.GetAllDemands();

            return demands;
        }

        [HttpGet("{userId}")]
        public IEnumerable<Demand> Get(int userId)
        {
            IEnumerable<Demand> demands = demandService.GetAllDemandsByUserId(userId);

            return demands;
        }

        [Authorize]
        [HttpPost]
        public Demand Post([FromBody] Demand demand)
        {
            return demandService.AddDemand(demand);
        }

        [HttpDelete]
        public void Delete([FromBody] Demand demand)
        {
            demandService.DeleteSingleDemand(demand);
        }

    }
}