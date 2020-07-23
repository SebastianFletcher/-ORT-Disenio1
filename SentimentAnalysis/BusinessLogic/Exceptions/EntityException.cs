using System;

namespace BusinessLogic.Exceptions
{
    public class EntityException : Exception
    {
        public EntityException(string error) : base(error) { }

    }
}
