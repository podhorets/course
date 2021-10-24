using AutoMapper;
using LearnIt.Courses.Domain.Entities;
using LearnIt.Courses.Domain.Models;

namespace LearnIt.Courses.Domain.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CourseRequestDto, Course>();

            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonRequestDto, Lesson>();
        }
    }
}