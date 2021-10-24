using System;
using System.Collections.Generic;
using LearnIt.Courses.Domain.Entities;

namespace LearnIt.Courses.Domain.Models
{
    public class CourseDto : AuditDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string FullDescription { get; set; }
        public string CoverImageUrl { get; set; }
        public string IntroductionVideoLink { get; set; }
        public List<Guid> Students { get; set; }
        public List<Lesson> Lessons { get; set; }
        public decimal Price { get; set; }
        
        
    }
}