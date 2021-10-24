using LearnIt.Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnIt.Courses.Data.Configuration
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(s => s.Title).IsRequired().HasMaxLength(128);
            builder.Property(s => s.Summary).IsRequired().HasMaxLength(512);
            builder.Property(s => s.FullDescription).HasMaxLength(2048);
            builder.Property(s => s.CoverImageUrl).HasMaxLength(128);
            builder.Property(s => s.IntroductionVideoLink).HasMaxLength(128);
        }
    }
}