using System;
using System.Runtime.Serialization;

namespace RepairCompanyManagement.BusinessLogic.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception innerException)
        : base(message, innerException)
        {
        }

        public ValidationException()
        {
        }

        protected ValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        
    }
}
