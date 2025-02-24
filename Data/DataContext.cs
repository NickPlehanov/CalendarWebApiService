using CalendarWebApiService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace CalendarWebApiService.Data
{
    public class DataContext :DbContext
    {
        public DbSet<Notes> Notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Конфигурируем подключение к PostgreSQL
            optionsBuilder.UseNpgsql("Host=localhost;Database=NotesDatabase;Username=postgres;Password=123456;");
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
