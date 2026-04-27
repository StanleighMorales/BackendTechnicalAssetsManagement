using System;

namespace BackendTechnicalAssetsManagement.src.Exceptions
{
    public class UserBlockedException : Exception
    {
        public UserBlockedException(string message) : base(message) { }
    }
}
