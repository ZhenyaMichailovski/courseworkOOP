using System;
using System.Runtime.Serialization;

namespace RepairCompanyManagement.BusinessLogic.Exceptions
{
    [Serializable]
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message)
            : base(message)
        {
        }

        public BusinessLogicException(string message, Exception innerException)
        : base(message, innerException)
        {
        }

        public BusinessLogicException()
        {
        }

        protected BusinessLogicException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
