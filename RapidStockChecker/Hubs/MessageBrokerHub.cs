using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace RapidStockChecker.Hubs
{
    public sealed class MessageBrokerHub : Hub
    {
        private readonly ILogger<MessageBrokerHub> logger;

        public MessageBrokerHub(ILogger<MessageBrokerHub> logger)
        {
            this.logger = logger;
        }

        public Task JoinGroup(string group)
        {
            Console.WriteLine($"User: {Context.ConnectionId} connected to {group}");
            logger.LogInformation($"User: {Context.ConnectionId} connected to {group}");
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
    }
}
