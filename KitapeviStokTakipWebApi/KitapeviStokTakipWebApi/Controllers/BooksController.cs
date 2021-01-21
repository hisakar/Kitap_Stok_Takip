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
    public class BooksController : Controller
    {
        IBookService bookService;
        
        public BooksController(IBookService _bookService)
        {
            this.bookService = _bookService;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            IEnumerable<Book> books = bookService.GetAllBooks();

            return books;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            Book book = bookService.GetBookById(id);

            return book;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Book Post([FromBody] Book book)
        {
            return bookService.AddBook(book);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public Book Put([FromBody] Book book)
        {
            return bookService.UpdateBook(book);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return bookService.DeleteBook(id);
        }
    }
}