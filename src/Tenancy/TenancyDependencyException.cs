using System.Runtime.Serialization;

namespace Tenancy
{
    [Serializable]
    internal class TenancyDependencyException : Exception
    {
        public TenancyDependencyException()
        {
        }

        public TenancyDependencyException(string? message) : base(message)
        {
        }

        public TenancyDependencyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TenancyDependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}