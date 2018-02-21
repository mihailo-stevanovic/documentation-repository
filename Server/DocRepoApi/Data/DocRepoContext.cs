using DocRepoApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Data
{
    public class DocRepoContext : DbContext
    {
        public DocRepoContext(DbContextOptions<DocRepoContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentAuthor> DocumentAuthors { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentUpdate> DocumentUpdates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVersion> ProductVersions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<DocumentAuthor>().ToTable("DocumentAuthor");
            modelBuilder.Entity<DocumentType>().ToTable("DocumentType");
            modelBuilder.Entity<DocumentUpdate>().ToTable("DocumentUpdate");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductVersion>().ToTable("ProductVersion");
        }

    }
}
