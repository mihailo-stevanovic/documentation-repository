﻿// <auto-generated />
using DocRepoApi.Data;
using DocRepoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DocRepoApi.Migrations
{
    [DbContext(typeof(DocRepoContext))]
    [Migration("20180221225036_AddParentDocument")]
    partial class AddParentDocument
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DocRepoApi.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("DocRepoApi.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AitId");

                    b.Property<string>("ClientCatalog");

                    b.Property<int>("DocumentTypeId");

                    b.Property<bool>("FitForClients");

                    b.Property<string>("HtmlLink");

                    b.Property<int>("LatestUpdateId");

                    b.Property<int?>("LatestUpdateId1");

                    b.Property<int?>("ParentDocumentId");

                    b.Property<string>("PdfLink");

                    b.Property<int>("ProductVersionId");

                    b.Property<string>("ShortDescription");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("WordLink");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("LatestUpdateId1");

                    b.HasIndex("ParentDocumentId");

                    b.HasIndex("ProductVersionId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("DocRepoApi.Models.DocumentAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("DocumentId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId")
                        .IsUnique();

                    b.HasIndex("DocumentId");

                    b.ToTable("DocumentAuthor");
                });

            modelBuilder.Entity("DocRepoApi.Models.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DocumentCategory");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("ShortName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DocumentType");
                });

            modelBuilder.Entity("DocRepoApi.Models.DocumentUpdate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DocumentId");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("LatestTopicsUpdated");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("DocumentUpdate");
                });

            modelBuilder.Entity("DocRepoApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("ShortName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("DocRepoApi.Models.ProductVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProductId");

                    b.Property<string>("Release")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVersion");
                });

            modelBuilder.Entity("DocRepoApi.Models.Document", b =>
                {
                    b.HasOne("DocRepoApi.Models.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DocRepoApi.Models.DocumentUpdate", "LatestUpdate")
                        .WithMany()
                        .HasForeignKey("LatestUpdateId1");

                    b.HasOne("DocRepoApi.Models.Document", "ParentDocument")
                        .WithMany()
                        .HasForeignKey("ParentDocumentId");

                    b.HasOne("DocRepoApi.Models.ProductVersion", "ProductVersion")
                        .WithMany()
                        .HasForeignKey("ProductVersionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DocRepoApi.Models.DocumentAuthor", b =>
                {
                    b.HasOne("DocRepoApi.Models.Author", "Author")
                        .WithOne("DocumentAuthor")
                        .HasForeignKey("DocRepoApi.Models.DocumentAuthor", "AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DocRepoApi.Models.Document", "Document")
                        .WithMany("DocumentAuthors")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DocRepoApi.Models.ProductVersion", b =>
                {
                    b.HasOne("DocRepoApi.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
