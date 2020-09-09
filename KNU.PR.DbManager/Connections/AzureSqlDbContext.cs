using KNU.PR.DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace KNU.PR.DbManager.Connections
{
    public class AzureSqlDbContext : DbContext
    {
        public DbSet<NewsEntity> NewsEntities { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<TagNewsEntity> TagsNews { get; set; }

        public AzureSqlDbContext(DbContextOptions<AzureSqlDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // NewsEntity
            modelBuilder.Entity<NewsEntity>().ToTable(nameof(NewsEntity));
            modelBuilder.Entity<NewsEntity>().HasKey(n => n.Id);
            modelBuilder.Entity<NewsEntity>().Property(n => n.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<NewsEntity>().Property(p => p.Content).HasColumnType("varchar(max)");

            // Tag
            modelBuilder.Entity<TagEntity>().ToTable(nameof(TagEntity));
            modelBuilder.Entity<TagEntity>().HasKey(t => t.Id);

            // TagsNews
            modelBuilder.Entity<TagNewsEntity>().ToTable(nameof(TagNewsEntity));
            modelBuilder.Entity<TagNewsEntity>().HasKey(t => t.Id);
            modelBuilder.Entity<TagNewsEntity>().HasOne(t => t.Tag).WithMany()
                .HasForeignKey(t => t.TagId);
            modelBuilder.Entity<TagNewsEntity>().HasOne(t => t.NewsEntity).WithMany()
                 .HasForeignKey(t => t.NewsEntityId);
        }
    }
}
