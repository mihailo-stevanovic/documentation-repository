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
    [Migration("20180318192011_ClientCatalogSeparateTable")]
    partial class ClientCatalogSeparateTable
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

                    b.Property<string>("AitName")
                        .HasMaxLength(10);

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(7);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsFormerAuthor");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("DocRepoApi.Models.ClientCatalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InternalId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ClientCatalog");
                });

            modelBuilder.Entity("DocRepoApi.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AitId");

                    b.Property<int>("DocumentTypeId");

                    b.Property<string>("HtmlLink");

                    b.Property<bool>("IsFitForClients");

                    b.Property<string>("OtherLink");

                    b.Property<int?>("ParentDocumentId");

                    b.Property<string>("PdfLink");

                    b.Property<int>("ProductVersionId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ShortDescription");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("WordLink");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

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

                    b.HasIndex("AuthorId");

                    b.HasIndex("DocumentId");

                    b.ToTable("DocumentAuthor");
                });

            modelBuilder.Entity("DocRepoApi.Models.DocumentCatalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CatalogId");

                    b.Property<int>("DocumentId");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.HasIndex("DocumentId");

                    b.ToTable("DocumentCatalog");
                });

            modelBuilder.Entity("DocRepoApi.Models.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DocumentCategory");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(5);

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

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("DocumentUpdate");
                });

            modelBuilder.Entity("DocRepoApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(7);

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("DocRepoApi.Models.ProductVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndOfSupport");

                    b.Property<int>("ProductId");

                    b.Property<string>("Release")
                        .IsRequired()
                        .HasMaxLength(10);

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

                    b.HasOne("DocRepoApi.Models.Document", "ParentDocument")
                        .WithMany("ChildDocuments")
                        .HasForeignKey("ParentDocumentId");

                    b.HasOne("DocRepoApi.Models.ProductVersion", "ProductVersion")
                        .WithMany()
                        .HasForeignKey("ProductVersionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DocRepoApi.Models.DocumentAuthor", b =>
                {
                    b.HasOne("DocRepoApi.Models.Author", "Author")
                        .WithMany("DocumentsAuthored")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DocRepoApi.Models.Document", "Document")
                        .WithMany("DocumentAuthors")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DocRepoApi.Models.DocumentCatalog", b =>
                {
                    b.HasOne("DocRepoApi.Models.ClientCatalog", "Catalog")
                        .WithMany()
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DocRepoApi.Models.Document", "Document")
                        .WithMany("DocumentCatalogs")
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
