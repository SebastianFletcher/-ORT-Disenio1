using System;

namespace BusinessLogic.Exceptions
{
    public class AnalysisException : Exception
    {
        public AnalysisException(string error) : base(error) { }
    }
}
