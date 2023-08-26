using System;
using System.Collections.Generic;

namespace CoreLib.Model
{
    public class OperationResult
    {
        public bool Success;
        public List<Exception> Exceptions;

        public OperationResult(bool success = true)
        {
            Success = success;
            Exceptions = new List<Exception>();
        }
    }
}
