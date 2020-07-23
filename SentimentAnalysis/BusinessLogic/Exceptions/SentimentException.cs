using System;

namespace BusinessLogic.Exceptions
{
    public class SentimentException : Exception
    {
        public SentimentException(string error) : base(error) { }
    }
}
