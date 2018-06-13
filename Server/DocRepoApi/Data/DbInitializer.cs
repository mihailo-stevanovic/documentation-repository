using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DocRepoContext context)
        {
            context.Database.EnsureCreated();

            if (context.DocumentUpdates.Any())
            {
                return;
            }


            var authors = new Author[]
            {
                    new Author { FirstName="Jamie", LastName="Smith", Alias="JSMIT", Email="jsmith@company.com", AitName = "Jamie", IsFormerAuthor = false },
                    new Author { FirstName="Ariel", LastName="Taylor", Alias="ATAYL", Email="ataylor@company.com", AitName = "Ariel", IsFormerAuthor = false },
                    new Author { FirstName="Glen", LastName="Williams", Alias="GWILL", Email="gwilliams@company.com", AitName = "Glen", IsFormerAuthor = false  },
                    new Author { FirstName="Shelby", LastName="Clark", Alias="SCLAR", Email="sclark@company.com", AitName = "Shelby", IsFormerAuthor = true  }
            };

            if (context.Authors.Count() < 4)
            {
                foreach (Author author in authors)
                {
                    context.Authors.Add(author);
                }

                context.SaveChanges();
            }

            var clientCatalogs = new ClientCatalog[]
                {
                    new ClientCatalog { Name = "Awesome Product - Release Notes" },
                    new ClientCatalog { Name = "Nice Product" },
                    new ClientCatalog { Name = "Old Product" },
                    new ClientCatalog { Name = "Mobile App" },
                    new ClientCatalog { Name = "CRM Solution" },
                    new ClientCatalog { Name = "Next Gen Portal" },
                    new ClientCatalog { Name = "Classic Portal" },
                    new ClientCatalog { Name = "Tools" },
                    new ClientCatalog { Name = "Framework" },
                    new ClientCatalog { Name = "Reporting" }
                };

            if (context.ClientCatalogs.Count() < 10)
            {

                foreach (ClientCatalog cat in clientCatalogs)
                {
                    context.ClientCatalogs.Add(cat);
                }

                context.SaveChanges();
            }



            if (context.DocumentTypes.Count() < 9)
            {
                var docTypes = new DocumentType[]
                {
                    new DocumentType { FullName="User Guide", ShortName="UG", DocumentCategory=DocumentCategory.FunctionalDocumentation },
                    new DocumentType { FullName="Configuration Guide", ShortName="CG", DocumentCategory=DocumentCategory.FunctionalDocumentation },
                    new DocumentType { FullName="Administrator Guide", ShortName="AG", DocumentCategory=DocumentCategory.FunctionalDocumentation },
                    new DocumentType { FullName="Reference Guide", ShortName="RG", DocumentCategory=DocumentCategory.FunctionalDocumentation },
                    new DocumentType { FullName="Knowledge Base", ShortName="KB", DocumentCategory=DocumentCategory.FunctionalDocumentation },
                    new DocumentType { FullName="Installation Guide", ShortName="IG", DocumentCategory=DocumentCategory.TechnicalDocumentation },
                    new DocumentType { FullName="System Requirements", ShortName="SR", DocumentCategory=DocumentCategory.TechnicalDocumentation },
                    new DocumentType { FullName="Technical Reference Guide", ShortName="TRG", DocumentCategory=DocumentCategory.TechnicalDocumentation },
                    new DocumentType { FullName="Release Notes", ShortName="RN", DocumentCategory=DocumentCategory.ReleaseNotes }
                };

                foreach (DocumentType docType in docTypes)
                {
                    context.DocumentTypes.Add(docType);
                }

                context.SaveChanges();
            }

            if (context.ProductVersions.Count() < 10)
            {
                var products = new Product[]
                {
                    new Product { ShortName = "P1" , FullName = "Awesome Product", Alias = "Product 1" },
                    new Product { ShortName = "P2" , FullName = "Nice Product", Alias = "Product 2" },
                    new Product { ShortName = "P3" , FullName = "Old Product", Alias = "Product 3" },
                    new Product { ShortName = "P4" , FullName = "Mobile App", Alias = "Product 4" },
                    new Product { ShortName = "P5" , FullName = "CRM Solution", Alias = "Product 5" },
                    new Product { ShortName = "P6" , FullName = "Next Gen Portal", Alias = "Product 6" },
                    new Product { ShortName = "P7" , FullName = "Classic Portal", Alias = "Product 7" },
                    new Product { ShortName = "P8" , FullName = "Reporting", Alias = "Product 8" }
                };

                foreach (Product product in products)
                {
                    context.Products.Add(product);
                }

                string[] versions = new string[] { "V2017.4", "V2017.5", "V2017.6", "V2017.7", "V2018.1", "V2018.2", "V2018.3", "V2018.4", "V2018.5" };

                foreach (string version in versions)
                {
                    var productVersions = Enumerable.Range(1, products.Count())
                        .Select(i => new ProductVersion { ProductId = i, Release = version, EndOfSupport = DateTime.Today.AddMonths(i * 2) });
                    context.AddRange(productVersions);
                }

                context.SaveChanges();
            }

            Document doc1 = new Document
            {
                Title = "Nice Product Release Notes",
                PdfLink = "NiceProduct/ReleaseNotes/NiceProduct_ReleaseNotes_V2018-1.pdf",
                DocumentTypeId = 9,
                IsFitForClients = true,
                ProductVersionId = 37,
                DocumentAuthors = new List<DocumentAuthor>
                {
                    new DocumentAuthor {Author = authors[0]},
                    new DocumentAuthor {Author = authors[1]}
                },
                DocumentCatalogs = new List<DocumentCatalog>
                {
                    new DocumentCatalog { Catalog =  clientCatalogs[1] }
                }

            };

            context.Documents.Add(doc1);


            Document doc2 = new Document
            {
                Title = "Mobile App Release Notes",
                PdfLink = "MobileApp/ReleaseNotes/MobileApp_ReleaseNotes_V2018-2.pdf",
                DocumentTypeId = 9,
                IsFitForClients = true,
                ProductVersionId = 15,
                DocumentAuthors = new List<DocumentAuthor>
                {
                    new DocumentAuthor {Author = authors[0]},
                    new DocumentAuthor {Author = authors[2]},
                    new DocumentAuthor {Author = authors[3]},
                },
                DocumentCatalogs = new List<DocumentCatalog>
                {
                    new DocumentCatalog { Catalog =  clientCatalogs[0] },
                    new DocumentCatalog { Catalog =  clientCatalogs[3] }
                }
            };
            context.Documents.Add(doc2);

            var docUpdates = new DocumentUpdate[]
            {
                new DocumentUpdate { Timestamp = DateTime.Parse("2018-01-01"), IsPublished = true, DocumentId = 1, LatestTopicsUpdated = "This is the first version of the document." },
                new DocumentUpdate { Timestamp = DateTime.Parse("2018-01-02"), IsPublished = true, DocumentId = 1, LatestTopicsUpdated = "Known Issues" },
                new DocumentUpdate { Timestamp = DateTime.Parse("2018-01-03"), IsPublished = true, DocumentId = 1, LatestTopicsUpdated = "Bug Fixes" },
                new DocumentUpdate { Timestamp = DateTime.Parse("2018-01-04"), IsPublished = false, DocumentId = 1, LatestTopicsUpdated = "V2018.1.1" },
                new DocumentUpdate { Timestamp = DateTime.Parse("2018-01-01"), IsPublished = true, DocumentId = 2, LatestTopicsUpdated = "This is the first version of the document." },
                new DocumentUpdate { Timestamp = DateTime.Parse("2018-01-02"), IsPublished = true, DocumentId = 2, LatestTopicsUpdated = "Known Issues" },
                new DocumentUpdate { Timestamp = DateTime.Parse("2018-01-03"), IsPublished = true, DocumentId = 2, LatestTopicsUpdated = "Bug Fixes" },
                new DocumentUpdate { Timestamp = DateTime.Parse("2018-01-04"), IsPublished = false, DocumentId = 2, LatestTopicsUpdated = "V2018.1.1" }
            };

            foreach (DocumentUpdate docUpdate in docUpdates)
            {
                context.DocumentUpdates.Add(docUpdate);
            }

            context.SaveChanges();

        }
    }
}
