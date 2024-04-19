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
            CreateMap<Book, BookUpdateModel>().ReverseMap();
            CreateMap<Publisher, PublisherModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<UserUpdateDTO, User>().ReverseMap();
            CreateMap<UserCreateDTO, User>().ReverseMap();
            CreateMap<Publisher, PublisherModel>().ReverseMap();
            CreateMap<PublisherUpdateDTO, Publisher>().ReverseMap();
            CreateMap<PublisherCreateDTO, Publisher>().ReverseMap();
            CreateMap<Author, AuthorModel>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorModel>().ReverseMap();
            CreateMap<Author,AddNewAuthorModel>().ReverseMap();
            CreateMap<BookAuthor, AddNewAuthorModel>().ReverseMap();

        }
    }
}
