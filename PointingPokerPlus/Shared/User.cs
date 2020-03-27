using System.ComponentModel.DataAnnotations;

namespace PointingPokerPlus.Shared
{
	public class User
	{
		[Key]
		public string Id { get; set; }
		public string Name { get; set; }
		public int Points { get; set; }

	}
}