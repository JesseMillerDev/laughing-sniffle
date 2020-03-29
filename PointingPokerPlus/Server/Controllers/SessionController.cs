using PointingPokerPlus.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PointingPokerPlus.Server.Data;

namespace PointingPokerPlus.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SessionController : ControllerBase
	{
		private readonly ILogger<SessionController> logger;
		private readonly PPPDBContext _context;

		public SessionController(ILogger<SessionController> logger, PPPDBContext context)
		{
			this.logger = logger;
			_context = context;
		}

		[HttpPost("startOrJoin")]
		public async Task<Session> StartOrJoin(StartJoinSessionRequest request)
		{
			if (string.IsNullOrEmpty(request.SessionId))
			{
				//Create Session
				var session = new Session();
				if(request.User != null)
				{
					session.Users.Add(request.User);
					await _context.AddAsync(session);
					await _context.SaveChangesAsync();
				}
				return session;
			}
			else
			{
				//Look up and join session
				var session = await _context.FindAsync<Session>(request.SessionId);
				session.Users.Add(request.User);
				_context.Update(session);
				await _context.SaveChangesAsync();
				return session;
			}
		}
	}
}
