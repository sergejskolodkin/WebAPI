using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
   


    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        BooksContext db;
        private readonly IMapper _mapper;
        
        public BooksController(BooksContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
            if (!db.Books.Any())
            {
                db.Books.Add(new Books { AuthorName = "Пушкин А.С", Genre = "Роман", TitleBook = "Капитанская Дочка" });
                db.Books.Add(new Books { AuthorName = "Есенин С.А", Genre = "Сказка", TitleBook = "Тетя Мотя" });
                db.Books.Add(new Books { AuthorName = "Жуковский В.А", Genre = "Баллада", TitleBook = "Людмила" });
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
            return Ok(_mapper.Map<IEnumerable<BooksDto>>(books));

        }

        // GET api/<BooksControllers>/5
        [HttpGet("{AuthorName}")]
        public async Task<ActionResult<BooksDto>> Get(string AuthorName)
        {
            var book = await db.Books.FirstOrDefaultAsync(x => x.AuthorName == AuthorName);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BooksDto>(book));
        }

   
        [HttpPost]
        public async Task<ActionResult<Books>> Post(BooksDto bookDto)
        {
            var booksModel = _mapper.Map<Books>(bookDto);
            if (booksModel == null)
            {
                return BadRequest();
            }

            db.Books.Add(booksModel);
            await db.SaveChangesAsync();
            return Ok(db.Books);
        }

        [HttpDelete("{AuthorName},{TitleBook}")]
        public async Task<ActionResult<Books>> Delete(string AuthorName, string TitleBook)
        {
            var book = db.Books.FirstOrDefault(x => x.AuthorName == AuthorName && x.TitleBook == TitleBook );

            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Ok(db.Books);
        }





        
    }
}
