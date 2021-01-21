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
    public class ChartsController : ControllerBase
    {
        IChartService chartService;
        public ChartsController(IChartService _chartService)
        {
            chartService = _chartService;
        }

        [HttpGet]
        public IEnumerable<Chart> GetCharts()
        {
            return chartService.GetAllChartItems();
        }

        [Authorize]
        [HttpPost]
        public bool AddChart([FromBody] Chart chart)
        {
            return chartService.AddChartItem(chart);
        }

        [HttpGet("{userId}")]
        public IEnumerable<Book> GetBooksByUserId(int userId)
        {
            return chartService.GetAllBooksByUserId(userId);
        }

        [HttpDelete]
        public void DeleteSingleChart([FromBody] Chart chart)
        {
            chartService.DeleteSingleChartItem(chart);
        }
    }
}