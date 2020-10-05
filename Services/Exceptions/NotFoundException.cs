using System;
namespace UniversityLectures.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message) 
        { 
        }
    }
}
