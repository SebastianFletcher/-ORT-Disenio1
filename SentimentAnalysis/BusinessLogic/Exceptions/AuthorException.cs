using System;

namespace BusinessLogic.Exceptions
{
    public class AuthorException : Exception
    {
        public AuthorException(string error) : base(error) { }
    }
}

