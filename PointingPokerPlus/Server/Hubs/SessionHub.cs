using Microsoft.AspNetCore.SignalR;
using PointingPokerPlus.Server.Data;
using PointingPokerPlus.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace PointingPokerPlus.Server.Hubs
{
	public class SessionHub : Hub
	{
		private readonly PPPDBContext _context;

		public SessionHub(PPPDBContext context)
		{
			_context = context;
		}
		public async Task SendPoints(string sessionId, string userId, int points)
		{
			var entity = _context.Sessions.FirstOrDefault(item => item.Id == sessionId);
			entity.Users.Find(u => u.Id == userId).Points = points;
			_context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
			_context.Update(entity);
			await _context.SaveChangesAsync();
			await Clients.All.SendAsync("ReceivePoints", userId, points);
		}
		
		public async Task<Session> JoinSession(string sessionId, User user)
		{
			//Find an existing session
			var session = await _context.Sessions.FindAsync(sessionId);
			
			//If the session exists do these things...
			if (session != null)
			{
				await Clients.Group(sessionId).SendAsync("UserJoined", user);
				await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);

				return session;
			}
			else
				return null;
		}
	}
}
