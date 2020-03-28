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

		public async Task CreateSession(Session session)
		{
			//create new session
			_context.Add(session);
			await _context.SaveChangesAsync();
		}
		public async Task<Session> JoinSession(string sessionId, string userId)
		{
			//join session by id
			var session = await _context.Sessions.FindAsync(sessionId);
			session.Users.Add(await _context.Users.FindAsync(userId));

			if (session != null)
			{
				return session;
			}
			else
				return null;
		}
	}
}
