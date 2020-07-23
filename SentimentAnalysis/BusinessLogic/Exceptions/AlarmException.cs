using System;

namespace BusinessLogic.Exceptions
{
    public class AlarmException : Exception
    {
        public AlarmException(string error) : base(error) { }
    }
}
