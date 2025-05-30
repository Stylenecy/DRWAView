using AutoMapper;
using SimpleRESTApi.Models;
using SimpleRESTApi.DTO;

namespace SimpleRESTApi.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Instructor, InstructorDTO>().ReverseMap();
            CreateMap<Course, CourseAddDTO>().ReverseMap();
            CreateMap<Course, CourseUpdateDTO>().ReverseMap();
            CreateMap<AspUserDTO, AspUserDTO>().ReverseMap();
            CreateMap<RegisterDTO, RegisterDTO>().ReverseMap();
            CreateMap<LoginDTO, LoginDTO>().ReverseMap();
        }
    }
}