using System.Runtime.Serialization;

namespace RoadStoryTracking.WebApi.Business.Models.Exceptions
{
    public class CustomAggregatedException : CustomApplicationException
    {
        public CustomApplicationException[] Exceptions { get; private set; }

        public CustomAggregatedException(string message, CustomApplicationException[] exceptions) : base(message)
        {
            Exceptions = exceptions;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Message), Message);
            info.AddValue("ClassName", GetType().Name);
            info.AddValue(nameof(Exceptions), Exceptions);
        }
    }
}