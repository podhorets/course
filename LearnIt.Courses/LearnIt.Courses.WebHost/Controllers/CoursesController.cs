using System;
using System.Net;
using System.Threading.Tasks;
using LearnIt.Courses.Domain.Models;
using LearnIt.Courses.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnIt.Courses.WebHost.Controllers
{
    [Route("courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService ?? throw new System.ArgumentNullException(nameof(courseService));
        }
        [HttpGet("{offsetPage:int}/{numberOfRows:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourses(int offsetPage, int numberOfRows)
        {
            var courses = await courseService.GetListOfCoursesAsync(offsetPage, numberOfRows);
            return Ok(courses);
        }

        [HttpGet("{courseId}/lessons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLessons(Guid courseId)
        {
            var lessons = await courseService.GetLessonsByCourseId(courseId);
            return Ok(lessons);
        }

        [HttpGet("{courseId}/lessons/{lessonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetLessonById(int lessonId)
        {
            var lesson = await courseService.GetLessonByIdAsync(lessonId);
            if(lesson == null) return NoContent();

            return Ok(lesson);
        }
        //[Authorize]
        [HttpPost("{courseId}/lessons")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLessonToTheCourse(Guid courseId, [FromBody]LessonRequestDto lesson)
        {
            lesson.CourseId = courseId;
            var lessonId = await courseService.AddLessonToCourse(lesson);
            return CreatedAtAction(
                        nameof(GetLessonById), 
                        new { courseId = lesson.CourseId, lessonId = lessonId }, 
                        lessonId);
        }
        //[Authorize]
        [HttpPut("{courseId}/lessons/{lessonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLesson(int lessonId, [FromBody]LessonRequestDto lesson)
        {
            await courseService.UpdateLessonAsync(lessonId, lesson);
            return Ok();
        }

        //[Authorize]
        [HttpDelete("{courseId}/lessons/{lessonId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveLesson(int lessonId)
        { 
            var model = await courseService.GetLessonByIdAsync(lessonId);
            if(model == null) return BadRequest();

            await courseService.RemoveLessonAsync(lessonId);
            return NoContent();
        }


        [HttpGet("{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCourseById(Guid courseId)
        {
            var course = await courseService.GetCourseById(courseId);
            if (course == null) return NoContent();

            return Ok(course);
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCourse([FromBody]CourseRequestDto course)
        {
            Guid courseId = await courseService.CreateCourseAsync(course);
            return CreatedAtAction(nameof(GetCourseById), new { courseId = courseId }, courseId);
        }

        //[Authorize]
        [HttpPut("{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCourse(Guid courseId, [FromBody]CourseRequestDto course)
        {
            var model = await courseService.GetCourseById(courseId);
            if(model == null) return NoContent();

            await courseService.UpdateCourseAsync(courseId, course);
            return Ok();
        }

        //[Authorize]
        [HttpDelete("{courseId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveCourse(Guid courseId)
        { 
            var model = await courseService.GetCourseById(courseId);
            if(model == null) return BadRequest();

            await courseService.RemoveCourseAsync(courseId);
            return NoContent();
        }
    }
}