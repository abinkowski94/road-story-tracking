using System;
using System.Runtime.Serialization;

namespace RoadStoryTracking.WebApi.Business.Models.Exceptions
{
    public class CustomApplicationException : ApplicationException
    {
        public object Reason { get; private set; }

        public CustomApplicationException(string message) : base(message)
        {
        }

        public CustomApplicationException(string message, object reason) : base(message)
        {
            Reason = reason;
        }

        public CustomApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Message), Message);
            info.AddValue("ClassName", GetType().Name);

            if (InnerException != null)
            {
                info.AddValue(nameof(InnerException), InnerException);
            }

            if (Reason != null)
            {
                info.AddValue(nameof(Reason), Reason);
            }
        }
    }
}