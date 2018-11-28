namespace RoadStoryTracking.WebApi.Business.Models.Messages
{
    public class EmailMessage
    {
        public string UserName { get; set; }

        public string CallbackUrl { get; set; }
        
        public string MainMessage { get; set; }
    }
}