using System;
using System.Linq;
using System.Text;

namespace PointingPokerPlus.Shared.Utilities
{
	public static class RandomGenerators
	{
		public static string GenerateSessionId()
		{
			StringBuilder builder = new StringBuilder();
			Enumerable
			   .Range(65, 26)
				.Select(e => ((char)e).ToString())
				.Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
				.Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
				.OrderBy(e => Guid.NewGuid())
				.Take(11)
				.ToList().ForEach(e => builder.Append(e));
			string id = builder.ToString();
			return id;
		}
	}
}
