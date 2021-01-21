using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitapeviStokTakipWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        IPublisherService publisherService;

        public PublishersController(IPublisherService _publisherService)
        {
            publisherService = _publisherService;
        }

        [HttpGet]
        public IEnumerable<Publisher> Get()
        {
            IEnumerable<Publisher> publishers = publisherService.GetAllPublishers();

            return publishers;

        }


        [HttpGet("{id}")]
        public Publisher Get(int id)
        {
            Publisher publisher = publisherService.GetPublisherById(id);

            return publisher;

        }

        [HttpPost]
        public Publisher Post([FromBody] Publisher publisher)
        {
            return publisherService.AddPublisher(publisher);
        }


        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return publisherService.DeletePublisher(id);
        }

    }
}