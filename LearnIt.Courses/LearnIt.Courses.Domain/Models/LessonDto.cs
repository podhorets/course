namespace LearnIt.Courses.Domain.Models
{
    public class LessonDto : AuditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public float Duration { get; set; }
        public bool ForFree { get; set; }
        
        
    }
}