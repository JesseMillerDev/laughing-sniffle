using PointingPokerPlus.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointingPokerPlus.Shared
{
	public class Session
	{
		[Key]
		public string Id { get; set; }
		public string Name { get; set; }
		[NotMapped]
		public SessionOptions Options { get; set; }
		public List<User> Users { get; set; }
		public User ActiveUser { get; set; }

		public Session()
		{
			Id = RandomGenerators.GenerateSessionId();
			Name = "New Session";
			Options = new SessionOptions { 
				PointSystem = new List<int> { 1, 2, 3, 5, 8, 13 }, 
				Options = new Dictionary<string, bool>() 
			};
			Users = new List<User>();
		}
	}
}
