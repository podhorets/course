using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnIt.Courses.Domain.Entities;
using LearnIt.Courses.Domain.Models;

namespace LearnIt.Courses.Domain.Services
{
    public interface ICourseService
    {
        Task<Guid> CreateCourseAsync(CourseRequestDto model);
        Task UpdateCourseAsync(Guid courseId, CourseRequestDto model);
        Task<IEnumerable<CourseDto>> GetListOfCoursesAsync(int offsetPage = 0, int numberOfRows = 10);
        Task RemoveCourseAsync(Guid courseId);
        Task<CourseDto> GetCourseById(Guid courseId);
        Task<int> AddLessonToCourse(LessonRequestDto lesson);
        Task UpdateLessonAsync(int lessonId, LessonRequestDto lesson);
        Task RemoveLessonAsync(int lessonId);
        Task<IEnumerable<LessonDto>> GetLessonsByCourseId(Guid courseId);
        Task<LessonDto> GetLessonByIdAsync(int lessonId);
        Task<IEnumerable<Guid>> GetStudentsByCourseId(Guid courseId);
        Task AddStudentToTheCourse(Guid courseId, Guid studentId);
        Task RemoveStudentFromTheCourse(Guid courseId, Guid studentId);
    }
}