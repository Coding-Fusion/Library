using AutoMapper;
using Core.Entites;
using Library.DTOS;
using LibraryBackend.Models;
using System.Runtime.InteropServices;

namespace Library.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Book, BookDTO>().ReverseMap()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

           
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();





            CreateMap<Reservation, ReservatoinDTO>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();

        }
    }
}
