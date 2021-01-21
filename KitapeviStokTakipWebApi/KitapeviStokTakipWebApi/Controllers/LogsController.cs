using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitapeviStokTakipWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        ILogService logService;

        public LogsController(ILogService _logService)
        {
            this.logService = _logService;
        }

        [HttpGet]
        [Route("category")]
        public IEnumerable<CategoryLog> GetCategoryLogs()
        {
            IEnumerable<CategoryLog> logs = logService.GetAllCategoryLogs();

            return logs;
        }

        //[HttpGet("category/{id}")] //not used yet
        //public CategoryLog GetCategory(int id) 
        //{
        //    CategoryLog log = logService.GetCategoryLogById(id);

        //    return log;
        //}

        [HttpDelete]
        [Route("category/{id}")]
        public int DeleteCategoryLog(int id)
        {
            return logService.DeleteCategoryLogById(id);
        }

        [Route("category")]
        [HttpDelete]
        public void DeleteAllCategoryLogs()
        {
            logService.DeleteAllCategoryLogs();
        }

        [HttpGet]
        [Route("book")]
        public IEnumerable<BookLog> GetBook()
        {
            IEnumerable<BookLog> logs = logService.GetAllBookLogs();

            return logs;
        }

        //[HttpGet("book/{id}")]  //not used yet
        //public BookLog GetBook(int id)
        //{
        //    BookLog log = logService.GetBookLogById(id);

        //    return log;
        //}

        [HttpDelete]
        [Route("book/{id}")]
        public int DeleteBookLog(int id)
        {
            return logService.DeleteBookLogById(id);
        }


        [HttpDelete]
        [Route("book")]
        public void DeleteAllBookLogs()
        {
            logService.DeleteAllBookLogs();
        }
    }
}