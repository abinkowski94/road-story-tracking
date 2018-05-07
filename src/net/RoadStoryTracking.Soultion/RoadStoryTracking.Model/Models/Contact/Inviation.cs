using System;

namespace RoadStoryTracking.Model.Models.Contact
{
    public class Inviation
    {
        public DateTimeOffset SendDate { get; set; }
        public Contact User { get; set; }
    }
}