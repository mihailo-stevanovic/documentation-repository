using DocRepoApi.Data;
using DocRepoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocRepoApiTests
{
    public static class DbTestContext
    {
        /// <summary>
        /// Generates a new InMemoryDbContext with data.
        /// </summary>
        /// <returns>Test database context with data.</returns>
        public static DocRepoContext GenerateContextWithData()
        {
            var options = new DbContextOptionsBuilder<DocRepoContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DocRepoContext(options);

            //var db = new DbContextOptionsBuilder();

            //db.UseInMemoryDatabase(Guid.NewGuid().ToString())
            //    .ReplaceService<IValueGeneratorCache, InMemoryValueGeneratorCache>();

            // Create and add test authors
            var authors = Enumerable.Range(1, 10)
                .Select(i => new Author
                {
                    Id = i,
                    Alias = $"AUTH{i}",
                    FirstName = $"First Name {i}",
                    LastName = $"Last Name {i}",
                    Email = $"dauthor{i}@domain.com",
                    AitName = $"user{i}",
                    IsFormerAuthor = (i % 2 == 0)
                });

            context.Authors.AddRange(authors);

            // Create and add test client catalogs
            var clientCatalogs = Enumerable.Range(1, 10)
                .Select(i => new ClientCatalog
                {
                    Id = i,
                    Name = $"Catalog {i}",
                    InternalId = $"C{i}"
                });

            context.ClientCatalogs.AddRange(clientCatalogs);

            // Create and add test products
            var products = Enumerable.Range(1, 10)
                .Select(i => new Product
                {
                    Id = i,
                    FullName = $"My Product {i}",
                    ShortName = $"MP{i}",
                    Alias = $"Old Name {i}"
                });

            context.Products.AddRange(products);

            // Create and add test products


            for (int i = 0; i < 10; i++)
            {
                var productVersions = Enumerable.Range(1, 10)
                .Select(j => new ProductVersion
                {
                    Id = j + i * 10,
                    Release = $"V{j}",
                    ProductId = i + 1,
                    EndOfSupport = DateTime.Today.AddMonths(j)
                });

                context.ProductVersions.AddRange(productVersions);
            }

            // Create and add test document types

            var docTypes = Enumerable.Range(1, 10)
                .Select(i => new DocumentType
                {
                    Id = i,
                    FullName = $"Doc Type{i}",
                    ShortName = $"DT{i}",
                    DocumentCategory = (DocumentCategory)(i < 5 ? i : i - 4) - 1
                });

            context.AddRange(docTypes);

            context.SaveChanges();

            // Create and add test documents

            List<Document> documents = new List<Document>();

            for (int i = 1; i <= 100; i++)
            {
                double x = i * 0.1;
                int foreignId = (int)Math.Ceiling(x);


                Document document = new Document
                {
                    Id = i,
                    AitId = i,
                    ProductVersionId = foreignId,
                    DocumentTypeId = foreignId,

                    DocumentAuthors = new List<DocumentAuthor>
                    {
                        new DocumentAuthor { AuthorId = foreignId },
                        new DocumentAuthor { AuthorId = foreignId + 1 }
                    },
                    DocumentCatalogs = new List<DocumentCatalog>
                    {
                        new DocumentCatalog { CatalogId = foreignId },
                        new DocumentCatalog { CatalogId = foreignId +1 }
                    },

                    IsFitForClients = (i % 2 == 0),

                    Title = $"Document {i}",
                    ShortDescription = $"This is Test Document #{i}",
                    HtmlLink = $"TestDocuments/{i}/HTML/index.htm",
                    PdfLink = $"TestDocuments/{i}/PDF/TestDocument_{i}.pdf",
                    WordLink = $"TestDocuments/{i}/DOC/TestDocument_{i}.doc",
                    Updates = Enumerable.Range(1, 10)
                            .Select(j => new DocumentUpdate
                            {
                                IsPublished = (i % 2 == 0),
                                LatestTopicsUpdated = $"Document {i} - Version {j}",
                                Timestamp = DateTime.Today.AddDays(j)

                            }).ToList()
                };

                if (i > 10)
                {
                    document.ParentDocumentId = foreignId;
                }

                documents.Add(document);
            }

            context.AddRange(documents);

            context.SaveChanges();
            return context;
        }

        /// <summary>
        /// Generates a new InMemoryDbContext without data.
        /// </summary>
        /// <returns>Test database context without data.</returns>
        public static DocRepoContext GenerateEmptyContext()
        {
            var options = new DbContextOptionsBuilder<DocRepoContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            return new DocRepoContext(options);
        }
    }
}
