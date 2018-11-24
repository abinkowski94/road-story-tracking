using RoadStoryTracking.WebApi.Business.Models.Messages;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.Messaging
{
    public interface IMessagingService
    {
        T PutImageMessageToQueue<T>(T message) where T : ImageMessage;
    }
}