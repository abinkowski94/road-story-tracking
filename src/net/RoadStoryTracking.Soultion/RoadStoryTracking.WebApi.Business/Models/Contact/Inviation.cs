using System;

namespace RoadStoryTracking.WebApi.Business.Models.Contact
{
    public class Inviation
    {
        public DateTimeOffset SendDate { get; set; }
        public Contact User { get; set; }
    }
}