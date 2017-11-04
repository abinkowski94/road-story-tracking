using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RoadStoryTracking.WebApi.Business.BusinessModels.Exceptions
{
    public class ValidationException : CustomApplicationException
    {
        public List<ValidationResult> Errors { get; private set; }

        public ValidationException(string message, List<ValidationResult> errors) : base(message)
        {
            Errors = errors;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Errors), Errors);
        }
    }
}