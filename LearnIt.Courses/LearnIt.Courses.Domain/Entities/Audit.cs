using System;

namespace LearnIt.Courses.Domain.Entities
{
    public abstract class Audit
    {
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Created { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}