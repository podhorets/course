using System.Data;
using FluentValidation;

namespace LearnIt.Courses.Domain.Models
{
    public class CourseRequestDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string FullDescription { get; set; }
        public string CoverImageUrl { get; set; }
        public string IntroductionVideoLink { get; set; }
        public decimal Price { get; set; }
    }
    public class CourseRequestDtoValidator : AbstractValidator<CourseRequestDto>
    {
        public CourseRequestDtoValidator()
        {
            RuleFor(s => s.Title).NotNull().MaximumLength(128);
            RuleFor(s => s.Summary).NotEmpty().MaximumLength(512);
            RuleFor(s => s.FullDescription).MaximumLength(2048);
            RuleFor(s => s.CoverImageUrl).MaximumLength(128);
            RuleFor(s => s.IntroductionVideoLink).MaximumLength(128);
        }
    }
}