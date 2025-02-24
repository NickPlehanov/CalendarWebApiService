using CalendarWebApiService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;

namespace CalendarWebApiService.Data
{
    public class DataContext :DbContext
    {
        private readonly AppSettings? _appSettings;
        public DataContext(IOptionsSnapshot<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public DbSet<Notes> Notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_appSettings.DatabaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка дополнительных параметров модели, если необходимо
            modelBuilder.Entity<Notes>()
            .Property(x => x.Id).IsRequired();
            modelBuilder.Entity<Notes>()
                .Property(x => x.Title).IsRequired();
        }
    }
}
