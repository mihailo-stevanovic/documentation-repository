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

            if (context.Documents.Any())
            {
                return;
            }


            var authors = new Author[]
            {
                    new Author { FirstName="Mihailo", LastName="Stevanović", Alias="MSTEV", Email="mstevanovic@efront.com" },
                    new Author { FirstName="Ivana", LastName="Matić", Alias="IMATI", Email="imatic@efront.com" },
                    new Author { FirstName="Milica", LastName="Prorok", Alias="MPROR", Email="mprorok@efront.com" }
            };

            if (context.Authors.Count() < 3)
            {
                foreach (Author author in authors)
                {
                    context.Authors.Add(author);
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
                    new Product { ShortName = "FIA" , FullName = "eFront Invest" },
                    new Product { ShortName = "FPM" , FullName = "eFront Portfolio Monitoring" },
                    new Product { ShortName = "PEO" , FullName = "eFront PEO/VC" },
                    new Product { ShortName = "FM" , FullName = "eFront Mobile" },
                    new Product { ShortName = "FO" , FullName = "eFront Outlook" },
                    new Product { ShortName = "ICX" , FullName = "eFront Investment Café" },
                    new Product { ShortName = "ICC" , FullName = "Investment Café Classic" },
                    new Product { ShortName = "INS" , FullName = "eFront Insight" }
                };

                foreach (Product product in products)
                {
                    context.Products.Add(product);
                }

                string[] versions = new string[] { "V2017.4", "V2017.5", "V2017.6", "V2017.7", "V2018.1", "V2018.2", "V2018.3", "V2018.4", "V2018.5" };

                foreach (string version in versions)
                {
                    var productVersions = Enumerable.Range(1, products.Count())
                        .Select(i => new ProductVersion { ProductId = i, Release = version });
                    context.AddRange(productVersions);
                }

                context.SaveChanges();
            }

            Document doc1 = new Document
            {
                Title = "eFront PM Release Notes",
                PdfLink = "eFrontPM/ReleaseNotes/eFrontPM_ReleaseNotes_V2018.1",
                DocumentTypeId = 9,
                FitForClients = true,
                ProductVersionId = 14,
                ClientCatalog = "eFront Portfolio Monitoring",                
                LatestUpdateId = 4
            };
            context.Documents.Add(doc1);


            Document doc2 = new Document
            {
                Title = "eFront Mobile Release Notes",
                PdfLink = "eFrontMobile/ReleaseNotes/eFrontPM_ReleaseNotes_V2018.1",
                DocumentTypeId = 9,
                FitForClients = true,
                ProductVersionId = 22,
                ClientCatalog = "eFront Mobile",                
                LatestUpdateId = 8
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
