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
    public class UsersController : ControllerBase
    {
        UsersContext db;
        private readonly IMapper _mapper;
        public UsersController(UsersContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
            if (!db.Users.Any())
            {
                db.Users.Add(new Users { Name = "Иван", Patronymic = "Иванович", Surname = "Иванов", BirthDate = new DateTime(2000, 3, 6) });
                db.Users.Add(new Users { Name = "Сергей", Patronymic = "Сергеевич", Surname = "Сергеев", BirthDate = new DateTime(2001, 4, 7) });
                db.Users.Add(new Users { Name = "Вадим", Patronymic = "Вадимович", Surname = "Вадимов", BirthDate = new DateTime(2002, 5, 8) });
                db.SaveChanges();
            }
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDto>>> Get() 
        {
            var users = await db.Users.ToListAsync();
            if (users.Count== 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<UsersDto>>(users)); ;
          
        }


        // GET api/<UsersController>/5
        [HttpGet("{Name}")]
        public async Task<ActionResult<UsersDto>> Get(string Name)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Name == Name);
            if (user==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UsersDto>(user));
        }


        //[HttpPost]
        //public async Task<ActionResult<Books>> Post(BooksDto bookDto)
        //{
        //    var booksModel = _mapper.Map<Books>(bookDto);
        //    if (booksModel == null)
        //    {
        //        return BadRequest();
        //    }

        //    db.Books.Add(booksModel);
        //    await db.SaveChangesAsync();
        //    return Ok(db.Books);
        //}


        
        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<Users>> Post(UsersDto userDto )
        {
            var usersModel = _mapper.Map<Users>(userDto);
            if (usersModel == null)
            {
                return BadRequest();
            }

            db.Users.Add(usersModel);
            await db.SaveChangesAsync();
            return Ok(db.Users);
        }



        // DELETE api/<UsersController>/5
        [HttpDelete("{Name},{Patronymic},{Surname}")]
        public async Task<ActionResult<Users>> Delete(string Name, string Patronymic, string Surname)
        {
            
            var user = db.Users.FirstOrDefault(x => x.Name == Name && x.Patronymic == Patronymic && x.Surname == Surname);
          

            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(db.Users); 
        }
    }
}
