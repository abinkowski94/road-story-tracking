using System;

namespace RoadStoryTracking.WebApi.Models
{
    public class Requestor
    {
        private readonly IServiceProvider _serviceProvider;

        public string UserName { get; private set; }

        public Requestor(string userName, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            UserName = userName;
        }
    }
}