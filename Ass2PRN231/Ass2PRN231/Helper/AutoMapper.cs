using AutoMapper;
using Repository.Entity;
using Repository.Models;
using System.Runtime.CompilerServices;

namespace Ass2PRN231.Helper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Author, AuthorModel>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorModel>().ReverseMap();
            CreateMap<Author,AddNewAuthorModel>().ReverseMap();
        }

    }
}
