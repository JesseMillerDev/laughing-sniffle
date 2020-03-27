using System;
using System.Collections.Generic;

namespace PointingPokerPlus.Shared
{
	public class SessionOptions
	{
		public Dictionary<string, bool> Options { get; set; }
		public List<int> PointSystem { get; set; }
	}
}