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
        public DbSet<TagClusterEntity> TagsClusters { get; set; }
        public DbSet<ClusterEntity> Clusters { get; set; }
        public DbSet<SubclusterEntity> Subclusters { get; set; }

        public AzureSqlDbContext(DbContextOptions<AzureSqlDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Cluster
            modelBuilder.Entity<ClusterEntity>().ToTable(nameof(ClusterEntity));
            modelBuilder.Entity<ClusterEntity>().HasKey(t => t.Id);

            // NewsEntity
            modelBuilder.Entity<NewsEntity>().ToTable(nameof(NewsEntity));
            modelBuilder.Entity<NewsEntity>().HasKey(n => n.Id);
            modelBuilder.Entity<NewsEntity>().Property(n => n.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<NewsEntity>().Property(p => p.Content).HasColumnType("varchar(max)");
            modelBuilder.Entity<NewsEntity>().HasOne(t => t.Cluster).WithMany().HasForeignKey(t => t.ClusterId);

            // Tag
            modelBuilder.Entity<TagEntity>().ToTable(nameof(TagEntity));
            modelBuilder.Entity<TagEntity>().HasKey(t => t.Id);

            // TagsClusters
            modelBuilder.Entity<TagClusterEntity>().ToTable(nameof(TagClusterEntity));
            modelBuilder.Entity<TagClusterEntity>().HasKey(t => t.Id);
            modelBuilder.Entity<TagClusterEntity>().HasOne(t => t.Tag).WithMany()
                .HasForeignKey(t => t.TagId);
            modelBuilder.Entity<TagClusterEntity>().HasOne(t => t.Cluster).WithMany()
                 .HasForeignKey(t => t.ClusterId);

            // Subcluster
            modelBuilder.Entity<SubclusterEntity>().ToTable(nameof(SubclusterEntity));
            modelBuilder.Entity<SubclusterEntity>().HasKey(t => t.Id);
            modelBuilder.Entity<SubclusterEntity>().HasOne(t => t.Child).WithMany()
                .HasForeignKey(t => t.ChildId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SubclusterEntity>().HasOne(t => t.Parent).WithMany()
                .HasForeignKey(t => t.ParentId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
