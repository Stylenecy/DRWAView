using AutoMapper;
using SimpleRESTApi.Models;
using SimpleRESTApi.DTO;

namespace SimpleRESTApi.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<Instructor, InstructorDTO>();
            CreateMap<Category, CategoryDTO>();
        }
    }
}