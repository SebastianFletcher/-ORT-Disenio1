using System;

namespace BusinessLogic.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException() : base("Can't access data source")
        {

        }
    }
}
