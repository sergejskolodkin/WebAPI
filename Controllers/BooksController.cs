using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
   


    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        BooksContext db;
        public BooksController(BooksContext context)
        {
            db = context;
            if (!db.Books.Any())
            {
                db.Books.Add(new BooksDto { AuthorName = "Пушкин А.С", Genre = "Роман", TitleBook = "Капитанская Дочка" });
                db.Books.Add(new BooksDto { AuthorName = "Есенин С.А", Genre = "Сказка", TitleBook = "Тетя Мотя" });
                db.Books.Add(new BooksDto { AuthorName = "Жуковский В.А", Genre = "Баллада", TitleBook = "Людмила" });
                db.SaveChanges();
            }
        }
        // GET: api/<BooksControllers>
        [HttpGet]
 
        public async Task<ActionResult<IEnumerable<BooksDto>>> Get()
        {
            var books = await db.Books.ToListAsync();
            if (books.Count == 0)
            {
                return NotFound();
            }
            return books;

        }

        // GET api/<BooksControllers>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BooksControllers>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BooksControllers>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksControllers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
