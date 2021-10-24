using System;
using System.Collections.Generic;

namespace LearnIt.Courses.Domain.Entities
{
    public class Course : Audit
    {
        public Course()
        {
            Students = new List<Guid>();
            Lessons = new List<Lesson>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Summary { get; set; }
        public string FullDescription { get; set; }
        public string CoverImageUrl { get; set; }
        public string IntroductionVideoLink { get; set; }
        public List<Guid> Students { get; set; }
        public List<Lesson> Lessons { get; set; }
        public Guid OwnerId { get; set; }
        public decimal Price { get; set; }   
    }
}