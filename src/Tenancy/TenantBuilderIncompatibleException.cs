using System.Runtime.Serialization;

namespace Outrage.Tenancy
{
    [Serializable]
    internal class TenantBuilderIncompatibleException : Exception
    {
        public TenantBuilderIncompatibleException()
        {
        }

        public TenantBuilderIncompatibleException(string? message) : base(message)
        {
        }

        public TenantBuilderIncompatibleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TenantBuilderIncompatibleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}