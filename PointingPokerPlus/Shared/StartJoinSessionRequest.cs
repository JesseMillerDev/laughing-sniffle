using System;
using System.Collections.Generic;
using System.Text;

namespace PointingPokerPlus.Shared
{
	public class StartJoinSessionRequest
	{
		public string SessionId { get; set; }
		public User User { get; set; }
	}
}
