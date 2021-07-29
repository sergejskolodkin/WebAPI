using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class AutoMapping:Profile
    {
        public AutoMapping() 
        {
            CreateMap<Books, BooksDto>();
            CreateMap<BooksDto, Books>();
            CreateMap<Users, UsersDto>();
            CreateMap<UsersDto, Users>();
        }
    }
}
