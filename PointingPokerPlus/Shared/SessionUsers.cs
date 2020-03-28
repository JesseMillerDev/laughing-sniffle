using System.ComponentModel.DataAnnotations;

namespace PointingPokerPlus.Shared
{
	public class SessionUsers
	{
		public string SessionId { get; set; }
		public Session Session { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
	}
}