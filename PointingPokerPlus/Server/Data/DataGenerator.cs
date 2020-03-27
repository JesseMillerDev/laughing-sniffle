using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PointingPokerPlus.Shared;
using System;
using System.Linq;

namespace PointingPokerPlus.Server.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PPPDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<PPPDBContext>>()))
            {
                if (context.Sessions.Any())
                {
                    return;   // Data was already seeded
                }

                context.Sessions.AddRange(new Session());

                context.SaveChanges();
            }
        }
    }
}
