using System;

namespace LearnIt.Courses.Domain.Exceptions
{
    public class EntityWasNotFoundException : Exception
    {
        public EntityWasNotFoundException(string message):base(message)
        {
            
        }
    }
}