using Microsoft.EntityFrameworkCore;
using PointingPokerPlus.Shared;
using System.Linq;

namespace PointingPokerPlus.Server.Data
{
	public class PPPDBContext : DbContext
	{
		public PPPDBContext(DbContextOptions<PPPDBContext> options)
			: base(options) 
		{
			ChangeTracker.LazyLoadingEnabled = false;
			Enumerable.ToList(Sessions.Include(s => s.Users));
		}

		public DbSet<Session> Sessions { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Session>()
				.HasMany(p => p.Users)
				.WithOne(p => p.Session);
			
		}
	}
}
