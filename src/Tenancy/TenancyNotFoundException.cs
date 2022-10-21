using System.Runtime.Serialization;

namespace Outrage.Tenancy
{
    [Serializable]
    internal class TenancyNotFoundException : Exception
    {
        public TenancyNotFoundException()
        {
        }

        public TenancyNotFoundException(string? message) : base(message)
        {
        }

        public TenancyNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TenancyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}