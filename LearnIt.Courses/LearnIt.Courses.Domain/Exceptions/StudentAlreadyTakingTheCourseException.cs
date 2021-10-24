using System;

namespace LearnIt.Courses.Domain.Exceptions
{
    public class StudentAlreadyTakingTheCourseException : Exception
    {
        public StudentAlreadyTakingTheCourseException(string message) : base(message)
        {
            
        }
    }
}