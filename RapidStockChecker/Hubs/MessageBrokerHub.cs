using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RapidStockChecker.Hubs
{
    public sealed class MessageBrokerHub : Hub
    {
        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
    }
}
