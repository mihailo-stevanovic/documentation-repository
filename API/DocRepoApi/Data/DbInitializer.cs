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
                    new ClientCatalog { Name = "Awesome Product" },
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

                List<KeyValuePair<string, DateTime>> versionsWithEndDate = new List<KeyValuePair<string, DateTime>>
                {
                    new KeyValuePair<string, DateTime>("V2017.1", DateTime.Parse("2018-01-01")),
                    new KeyValuePair<string, DateTime>("V2017.2", DateTime.Parse("2018-03-01")),
                    new KeyValuePair<string, DateTime>("V2017.3", DateTime.Parse("2018-05-01")),
                    new KeyValuePair<string, DateTime>("V2017.4", DateTime.Parse("2018-07-01")),
                    new KeyValuePair<string, DateTime>("V2017.5", DateTime.Parse("2018-09-01")),
                    new KeyValuePair<string, DateTime>("V2017.6", DateTime.Parse("2018-11-01")),
                    new KeyValuePair<string, DateTime>("V2018.1", DateTime.Parse("2019-01-01")),
                    new KeyValuePair<string, DateTime>("V2018.2", DateTime.Parse("2019-03-01")),
                    new KeyValuePair<string, DateTime>("V2018.3", DateTime.Parse("2019-05-01")),
                    new KeyValuePair<string, DateTime>("V2018.4", DateTime.Parse("2019-07-01")),
                    new KeyValuePair<string, DateTime>("V2018.5", DateTime.Parse("2019-09-01")),
                    new KeyValuePair<string, DateTime>("V2018.6", DateTime.Parse("2019-11-01"))

                };
                

                foreach (KeyValuePair<string, DateTime> version in versionsWithEndDate)
                {
                    var productVersions = Enumerable.Range(1, products.Count())
                        .Select(i => new ProductVersion { ProductId = i, Release = version.Key, EndOfSupport = version.Value});
                    context.AddRange(productVersions);
                }

                context.SaveChanges();
            }

            List<Product> productList = context.Products.ToList();
            List<DocumentType> docTypeList = context.DocumentTypes.ToList();
            ClientCatalog catalogFramework = context.ClientCatalogs.SingleOrDefault(cat => cat.Name == "Framework");

            foreach (Product product in productList)
            {
                ClientCatalog catalog = context.ClientCatalogs.SingleOrDefault(cat => cat.Name == product.FullName);

                if (catalog == null)
                {
                    catalog = context.ClientCatalogs.SingleOrDefault(cat => cat.Name == "Tools");
                }                

                List<ProductVersion> versions = context.ProductVersions.Where(v => v.ProductId == product.Id).ToList();
                                
                foreach (ProductVersion version in versions)
                {
                    foreach (DocumentType docType in docTypeList)
                    {
                        string rootDir = $"{product.FullName.Replace(" ", string.Empty)}/{docType.FullName.Replace(" ", string.Empty)}";
                        string printFileName = $"{product.FullName.Replace(" ", string.Empty)}_{docType.FullName.Replace(" ", string.Empty)}_{version.Release}";

                        Document document = new Document
                        {
                            Title = $"{product.FullName} {docType.FullName}",
                            ProductVersion = version,
                            DocumentType = docType,
                            DocumentAuthors = new List<DocumentAuthor>
                            {
                                new DocumentAuthor { Author = authors[0] },
                                new DocumentAuthor { Author = authors[1] },
                                new DocumentAuthor { Author = authors[2] }
                            },
                            HtmlLink = $"{rootDir}/HTML_{version.Release}/index.html",
                            PdfLink = $"{rootDir}/PDF_{version.Release}/{printFileName}.pdf",
                            WordLink = $"{rootDir}/Word_{version.Release}/{printFileName}.docx",

                            IsFitForClients = docType.ShortName != "CG" && docType.ShortName != "AG",
                            ShortDescription = $"The document contains the full {docType.FullName} for the {version.Release} of {product.FullName}",

                            Updates = Enumerable.Range(1, 10)
                                .Select(j => new DocumentUpdate
                                {                                    
                                    IsPublished = (j % 3 != 0),
                                    LatestTopicsUpdated = $"This is version {j} of the document.",
                                    Timestamp = version.EndOfSupport.AddYears(-1).AddMonths(j - 1)
                                }).ToList(),

                            DocumentCatalogs = new List<DocumentCatalog>
                            {
                                new DocumentCatalog { Catalog = catalog },
                                new DocumentCatalog { Catalog = catalogFramework }
                            }
                        };

                        context.Add(document);
                    }
                }
            }
            
            context.SaveChanges();

        }
    }
}
