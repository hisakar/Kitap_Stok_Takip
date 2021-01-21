using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {

        ICategoryService categoryService;

        public CategoriesController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            IEnumerable<Category> categories = categoryService.GetAllCategories();

            return categories;
           
        }

        [HttpGet("{id}")]
        public Category Get(int id )
        {
           Category category = categoryService.GetCategoryById(id);

            return category;

        }

        [HttpPost]
        public Category Post([FromBody] Category category)
        {
            return categoryService.AddCategory(category);
        }

        [HttpPut]
        public Category Put([FromBody] Category category)
        {
             return categoryService.UpdateCategory(category);
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return categoryService.DeleteCategory(id);
        }

    }


}

