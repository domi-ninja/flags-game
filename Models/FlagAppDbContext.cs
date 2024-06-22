using System.Text.Json;
using CoreFlags;
using flags_game.Pages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace flags_game.Models
{
    public class FlagAppDbContext : DbContext
    {
        public DbSet<Flag> flags { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<FlagTag> flagTags { get; set; }
        public DbSet<ColorTag> colorTags { get; set; }
        public DbSet<FlagColor> flagColor { get; set; }

        public string dbPath { get; private set; }

        public FlagAppDbContext(DbContextOptions<FlagAppDbContext> options) : base(options)
        {

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

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }


    public class Flag
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string url { get; set; }

        public List<FlagTag> flagTags { get; set; } = new List<FlagTag>();
        public long population { get; set; }
        public List<ColorTag> colorTags { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public string constructEmoji()
        {
            string country = this.shortName;

            return string.Concat(country.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
        }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<FlagTag> flagTags { get; set; } = new List<FlagTag>();

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
