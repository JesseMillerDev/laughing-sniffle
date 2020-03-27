using Microsoft.EntityFrameworkCore;
using PointingPokerPlus.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointingPokerPlus.Server.Data
{
	public class PPPDBContext : DbContext
	{
		public PPPDBContext(DbContextOptions<PPPDBContext> options)
			: base(options) { }

		public DbSet<Session> Sessions { get; set; }

	}
}
