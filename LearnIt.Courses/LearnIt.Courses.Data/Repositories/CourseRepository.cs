using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LearnIt.Courses.Domain.Entities;
using LearnIt.Courses.Domain.Exceptions;
using LearnIt.Courses.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace LearnIt.Courses.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CoursesDbContext context;

        public CourseRepository(CoursesDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<int> AddLessonToCourseAsync(Lesson lesson)
        {
            //check if course exist
            await GetCourseByIdAsync(lesson.CourseId);

            await context.Lessons.AddAsync(lesson);
            await context.SaveChangesAsync();
            return lesson.Id;
        }

        public async Task AddStudentToTheCourse(Guid courseId, Guid studentId)
        {
            var course = await GetCourseByIdAsync(courseId);

            if (course.Students.Contains(studentId)) 
                throw new StudentAlreadyTakingTheCourseException($"Student {studentId} already taking class id {courseId}");
            
            course.Students.Add(studentId);
            context.Update(course);
            await context.SaveChangesAsync();
        }

        public async Task<Guid> CreateCourseAsync(Course course)
        {
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();
            return course.Id;
        }

        public async Task<Course> GetCourseByIdAsync(Guid courseId)
        {
            var course = await context.Courses.FirstOrDefaultAsync(s => s.Id == courseId);
            if(course == null)
                throw new EntityWasNotFoundException($"Course id {courseId} was not found");

            return course;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(int offsetPage = 0, int numberOfRows = 10)
        {
            return await context.Courses
                                .OrderByDescending(s=>s.Created)
                                .Skip(offsetPage)
                                .Take(numberOfRows)
                                .ToListAsync();
        }

        public async Task<Lesson> GetLessonByIdAsync(int id)
        {
            return await context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Lesson>> GetLessonsByCourseIdAsync(Guid courseId)
        {
            return await context.Lessons
                        .Where(l => l.CourseId == courseId)
                        .OrderBy(s=>s.Order)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetStudentsByCourseIdAsync(Guid courseId)
        {
            var course = await GetCourseByIdAsync(courseId);
            return course.Students;
        }

        public async Task RemoveCourseById(Guid courseId)
        {
            var entity = await context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            context.Courses.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveLessonByIdAsync(int lessonId)
        {
            var entity = await context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
            context.Lessons.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveStudentFromTheCourse(Guid courseId, Guid studentId)
        {
            var course = await GetCourseByIdAsync(courseId);
            course.Students.Remove(studentId);
            context.Update(course);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            context.Update(course);
            await context.SaveChangesAsync();
        }

        public async Task UpdateLessonAsync(Lesson lesson)
        {
            context.Update(lesson);
            await context.SaveChangesAsync();
        }
    }
}