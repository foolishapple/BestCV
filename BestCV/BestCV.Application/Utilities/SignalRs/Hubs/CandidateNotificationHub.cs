using BestCV.Application.Utilities.SignalRs.Extensions;
using BestCV.Application.Models.CandidateNotifications;
using Microsoft.AspNetCore.SignalR;

namespace BestCV.Application.Utilities.SignalRs.Hubs
{
    public class CandidateNotificationHub : Hub 
    {
        public static IHubCallerClients CacheClients { get; set; } = default!;
        public static HashSet<(long UserId, string ConnectionId)> _connecteds = new();
        public async Task SendNotifications(CandidateNotificationDTO obj)
        {
            if (obj.CandidateId == null)
            {
                await CacheClients.All.SendAsync("SendNotifications", obj);
            }
            else
            {
                var connectedIds = _connecteds.Where(c => c.UserId == obj.CandidateId).Select(c => c.ConnectionId).ToList();
                foreach (var item in connectedIds)
                {
                    await CacheClients.Client(item).SendAsync("SendNotifications", obj);
                }
            }
        }
        //Execute when user connect to signalR
        public override async Task OnConnectedAsync()
        {
            CacheClients = this.Clients;
            var connectionId = Context.ConnectionId;
            long userId = this.GetLoggedInUserId();
            _connecteds.Add((userId, connectionId));
            await base.OnConnectedAsync();
        }
        //Execute when user disconnect from signalR
        public override async Task OnDisconnectedAsync(Exception e)
        {
            var connectionId = Context.ConnectionId;
            var connect = _connecteds.FirstOrDefault(c => c.ConnectionId == connectionId);
            //Remove that conncetionId from dictionary containing all connections
            _connecteds.Remove(connect);
            await base.OnDisconnectedAsync(e);
        }
    }
}
