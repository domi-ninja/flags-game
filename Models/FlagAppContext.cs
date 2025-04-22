using CoreFlags;
using Microsoft.EntityFrameworkCore;

namespace flags_game.Models
{
    public class FlagAppContext: DbContext 
    {

        public DbSet<Flag> flags { get; set; }

        public string dbPath { get; private set; }

        public FlagAppContext()
        {
            dbPath = "flags.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}
