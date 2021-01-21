using KitapeviStokTakipWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.IServices
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        Category AddCategory(Category category);

        Category UpdateCategory(Category category);

        int DeleteCategory(int id);
    }
}
