using KitapeviStokTakipWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.IServices
{
    public interface IPublisherService
    {
        IEnumerable<Publisher> GetAllPublishers();
        Publisher GetPublisherById(int id);
        Publisher AddPublisher(Publisher publisher);

        int DeletePublisher(int id);
    }
}
