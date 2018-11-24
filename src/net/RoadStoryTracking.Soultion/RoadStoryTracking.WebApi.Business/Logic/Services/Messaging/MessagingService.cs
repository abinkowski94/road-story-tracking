using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using RoadStoryTracking.WebApi.Business.Models.Messages;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.Messaging
{
    public class MessagingService : IMessagingService
    {
        private readonly string _imageQueueConnectionString;
        private readonly string _imageQueueName;

        public MessagingService(IConfiguration configuration)
        {
            _imageQueueName = configuration["Storage:Queues:Default:QueueName"]
                ?? throw new ApplicationException("The key 'Storage:Queues:Default:QueueName' is not registered");

            _imageQueueConnectionString = configuration["Storage:Queues:Default:ConnectionString"]
                ?? throw new ApplicationException("The key 'Storage:Queues:Default:ConnectionString' is not registered");
        }

        public T PutImageMessageToQueue<T>(T message) where T : ImageMessage
        {
            var task = Task.Run(async () =>
            {
                // Create a message and add it to the queue.
                var queue = await GetImageQueue();
                var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(message));
                await queue.AddMessageAsync(queueMessage);

                return message;
            });

            task.Wait();

            return task.Result;
        }

        private async Task<CloudQueue> GetImageQueue()
        {
            // Retrieve storage account from connection string.
            var storageAccount = CloudStorageAccount.Parse(_imageQueueConnectionString);

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            var queue = queueClient.GetQueueReference(_imageQueueName);

            // Create the queue if it doesn't already exist.
            await queue.CreateIfNotExistsAsync();

            return queue;
        }
    }
}