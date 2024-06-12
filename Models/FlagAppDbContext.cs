using CoreFlags;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace flags_game.Models
{
    public class FlagAppDbContext : DbContext
    {
        public DbSet<Flag> flags { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<FlagTag> flagTags { get; set; }

        public string dbPath { get; private set; }

        public FlagAppDbContext(DbContextOptions<FlagAppDbContext> options) : base(options)
        {
            //dbPath = "flags.db";
        }

        // dotnet tool install --global dotnet-ef
        // dotnet ef migrations add InitialCreate --context FlagAppContext --output-dir Migrations/SqlServerMigration
        // https://learn.microsoft.com/en-us/ef/core/cli/dotnet

        //protected override void OnConfiguring(DbContextOptionsBuilder dcob)
        //{
        //    dcob.UseSqlite(dbPath);
        //}
    }

    public class FlagTag
    {
        public Tag Tag { get; set; }
        public int Id { get; set; }
        public int FlagId { get; set; }
        public int TagId { get; set; }
        public Flag Flag { get; set; }
    }


    public class Flag
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public List<FlagTag> flagTags { get; set; } = new List<FlagTag>();
        public long population { get; internal set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<FlagTag> flagTags { get; set; } = new List<FlagTag>();

    }
}
