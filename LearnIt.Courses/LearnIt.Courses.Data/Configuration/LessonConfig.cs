using LearnIt.Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnIt.Courses.Data.Configuration
{
    public class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(s => s.Title).IsRequired().HasMaxLength(128);
            builder.Property(s => s.Content).HasMaxLength(4000);
        }
    }
}