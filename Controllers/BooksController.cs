using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
   


    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<BooksDto> _books = new List<BooksDto>()
        {
           new BooksDto { AuthorName = "Пушкин А.С", Genre = "Роман", TitleBook = "Капитанская Дочка" },
           new BooksDto { AuthorName = "Есенин С.А", Genre = "Сказка", TitleBook = "Тетя Мотя" },
           new BooksDto { AuthorName = "Жуковский В.А", Genre = "Баллада", TitleBook = "Людмила" }
        };
        // GET: api/<BooksControllers>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksDto>>> GetTodoItems() 
        {
            return _books;
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
