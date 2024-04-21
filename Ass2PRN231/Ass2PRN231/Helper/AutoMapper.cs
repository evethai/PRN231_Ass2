using AutoMapper;
using Repository.Entity;
using Repository.Models;
using Repository.Models.AuthorModels;
using System.Runtime.CompilerServices;

namespace Ass2PRN231.Helper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Book, BookUpdateModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserUpdateModel>().ReverseMap();            
            CreateMap<Publisher, PublisherModel>().ReverseMap();
            CreateMap<Publisher, PublisherUpdateModel>().ReverseMap();          
            CreateMap<Author, AuthorModel>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorModel>().ReverseMap();
            CreateMap<Author,AddNewAuthorModel>().ReverseMap();
            CreateMap<BookAuthor, AddNewAuthorModel>().ReverseMap();


            CreateMap<Author, GetAuthorModel>().ReverseMap();    

        }
    }
}
