using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite.Classes
{
    public class Core : DbContext
    {
        public DbSet<Post> Post { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=46.146.233.70;database=ezhupa;user=debohih;password=analny");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreating(modelBuilder);
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Header).IsRequired();
                entity.HasOne(d => d.Publisher)
                  .WithMany(p => p.Posts);
            });
        }
    }
}
