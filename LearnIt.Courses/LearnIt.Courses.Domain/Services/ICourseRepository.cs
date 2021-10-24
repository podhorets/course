using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnIt.Courses.Domain.Entities;

namespace LearnIt.Courses.Domain.Services
{
    public interface ICourseRepository
    {
        Task<Course> GetCourseByIdAsync(Guid courseId);
        Task UpdateCourseAsync(Course course);
        Task<Guid> CreateCourseAsync(Course course);
        Task RemoveCourseById(Guid courseId);
        Task<IEnumerable<Course>> GetCoursesAsync(int offsetPage = 0, int numberOfRows = 10);
        Task<int> AddLessonToCourseAsync(Lesson lesson);
        Task UpdateLessonAsync(Lesson lesson);
        Task RemoveLessonByIdAsync(int lessonId);
        Task<IEnumerable<Lesson>> GetLessonsByCourseIdAsync(Guid courseId);
        Task<Lesson> GetLessonByIdAsync(int id);
        Task<IEnumerable<Guid>> GetStudentsByCourseIdAsync(Guid courseId);
        Task AddStudentToTheCourse(Guid courseId, Guid studentId);
        Task RemoveStudentFromTheCourse(Guid courseId, Guid studentId);
    }
}