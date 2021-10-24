using System;

namespace LearnIt.Courses.Domain.Models
{
    public abstract class AuditDto
    {
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Created { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}