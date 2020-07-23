using System;

namespace BusinessLogic.Exceptions
{
    public class PhraseException : Exception
    {
        public PhraseException(string error) : base(error) { }
    }
}
