using System.Runtime.Serialization;

namespace Tenancy.Providers
{
    [Serializable]
    internal class TenantNotSpecifiedException : Exception
    {
        public TenantNotSpecifiedException()
        {
        }

        public TenantNotSpecifiedException(string? message) : base(message)
        {
        }

        public TenantNotSpecifiedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TenantNotSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}