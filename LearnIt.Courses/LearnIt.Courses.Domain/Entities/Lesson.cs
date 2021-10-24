using System;

namespace LearnIt.Courses.Domain.Entities
{
    public class Lesson : Audit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public float Duration { get; set; }
        public Guid CourseId { get; set; }
        public bool ForFree { get; set; }
    }
}