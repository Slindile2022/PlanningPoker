using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace PlanningPoker.Hubs
{
	public class PlanningPokerHub : Hub
	{
		public async Task JoinSession(string sessionId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
		}

		public async Task LeaveSession(string sessionId)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);
		}
	}
}
