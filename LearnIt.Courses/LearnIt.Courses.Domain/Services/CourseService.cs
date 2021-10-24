using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LearnIt.Courses.Domain.Entities;
using LearnIt.Courses.Domain.Exceptions;
using LearnIt.Courses.Domain.Models;

namespace LearnIt.Courses.Domain.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> AddLessonToCourse(LessonRequestDto lesson)
        {
            var entity = mapper.Map<Lesson>(lesson);
            return await courseRepository.AddLessonToCourseAsync(entity);
        }

        public async Task AddStudentToTheCourse(Guid courseId, Guid studentId)
        {
            await courseRepository.AddStudentToTheCourse(courseId, studentId);
        }

        public async Task<Guid> CreateCourseAsync(CourseRequestDto model)
        {
            var entity = mapper.Map<Course>(model);
            return await courseRepository.CreateCourseAsync(entity);
        }

        public async Task<CourseDto> GetCourseById(Guid courseId)
        {
            var entity = await courseRepository.GetCourseByIdAsync(courseId);
            return mapper.Map<CourseDto>(entity);
        }

        public async Task<LessonDto> GetLessonByIdAsync(int lessonId)
        {
            var lesson = await courseRepository.GetLessonByIdAsync(lessonId);
            return mapper.Map<LessonDto>(lesson);
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByCourseId(Guid courseId)
        {
            var lessons = await courseRepository.GetLessonsByCourseIdAsync(courseId);
            return mapper.Map<IEnumerable<LessonDto>>(lessons);
        }

        public async Task<IEnumerable<CourseDto>> GetListOfCoursesAsync(int offsetPage = 0, int numberOfRows = 10)
        {
            var entities = await courseRepository.GetCoursesAsync(offsetPage, numberOfRows);
            return mapper.Map<IEnumerable<CourseDto>>(entities);
        }

        public async Task<IEnumerable<Guid>> GetStudentsByCourseId(Guid courseId)
        {
            return await courseRepository.GetStudentsByCourseIdAsync(courseId);
        }

        public async Task RemoveCourseAsync(Guid courseId)
        {
            await courseRepository.RemoveCourseById(courseId);
        }

        public async Task RemoveLessonAsync(int lessonId)
        {
            await courseRepository.RemoveLessonByIdAsync(lessonId);
        }

        public Task RemoveStudentFromTheCourse(Guid courseId, Guid studentId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCourseAsync(Guid courseId, CourseRequestDto model)
        {
            var entity = await courseRepository.GetCourseByIdAsync(courseId);
            if(entity == null) throw new EntityWasNotFoundException("Course was not found");

            mapper.Map(model, entity);
            await courseRepository.UpdateCourseAsync(entity);
        }

        public async Task UpdateLessonAsync(int lessonId, LessonRequestDto lesson)
        {
            var entity = await courseRepository.GetLessonByIdAsync(lessonId);
            if(entity == null) throw new EntityWasNotFoundException("Lesson was not found");
            mapper.Map(lesson, entity);
            await courseRepository.UpdateLessonAsync(entity);
        }
        
    }
}