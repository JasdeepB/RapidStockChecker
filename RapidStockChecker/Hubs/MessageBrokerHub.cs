using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace RapidStockChecker.Hubs
{
    public sealed class MessageBrokerHub : Hub
    {
        public Task JoinGroup(string group)
        {
            Console.WriteLine($"User: {Context.ConnectionId} connected to {group}");
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
    }
}
