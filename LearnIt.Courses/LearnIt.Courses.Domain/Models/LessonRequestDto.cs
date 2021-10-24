using FluentValidation;
using System;

namespace LearnIt.Courses.Domain.Models
{
    public class LessonRequestDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public float Duration { get; set; }
        public Guid CourseId { get; set; }
        public bool ForFree { get; set; }
        
        
    }
    public class LessonRequestDtoValidator : AbstractValidator<LessonRequestDto>
    { 
        public LessonRequestDtoValidator()
        {
            RuleFor(s => s.Title).NotEmpty().MaximumLength(128);
            RuleFor(s => s.Content).NotEmpty().MaximumLength(4000);
        }
    }
}