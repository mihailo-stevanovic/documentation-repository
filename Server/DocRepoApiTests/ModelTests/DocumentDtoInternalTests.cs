using AutoMapper;
using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ModelTests
{
    public class DocumentDtoInternalTests
    {
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        #region Test Compare and Sort
        [Fact(DisplayName = "DocumentDtoInternal.Equals(other, true) should match based on ID and all properties")]
        public void DocumentDtoInternalEqualsReturnsCorrectValues()
        {
            DocumentDtoInternal d1 = new DocumentDtoInternal
            {
                Id = 1,
                Title = "Document 1",
                ShortDescription = "This is a test document.",

                Product = "Product 1",
                Version = "V2018.1",
                Authors = new List<AuthorDto>
                {
                    new AuthorDto {
                        Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
                    },
                    new AuthorDto {
                        Id = 2,
                Alias = "SALIA",
                AitName = "Writer",
                FirstName = "Writer",
                LastName = "Tech",
                Email = "tech2@writer.com",
                IsFormerAuthor = false
                    }
                },
                ClientCatalogs = new List<ClientCatalogDto>
                {
                    new ClientCatalogDto
                    {
                        Id = 1,
                Name = "Client Cat 1",
                InternalId = "CT1"
                    },
                    new ClientCatalogDto
                    {
                        Id = 2,
                Name = "Client Cat 2",
                InternalId = "CT2"
                    }
                },
                DocumentType = "User Guide",

                HtmlLink = "document1/html/index.htm",
                PdfLink = "document1/pdf/document1.pdf",
                WordLink = "document1/word/document1.doc",
                OtherLink = null,

                IsFitForClients = true,
                LatestTopicsUpdated = "This is the first version of the document."

            };

            DocumentDtoInternal d2 = new DocumentDtoInternal
            {
                Id = 1,
                Title = "Document 1",
                ShortDescription = "This is a test document.",

                Product = "Product 1",
                Version = "V2018.1",
                Authors = new List<AuthorDto>
                {
                    new AuthorDto {
                        Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
                    },
                    new AuthorDto {
                        Id = 2,
                Alias = "SALIA",
                AitName = "Writer",
                FirstName = "Writer",
                LastName = "Tech",
                Email = "tech2@writer.com",
                IsFormerAuthor = false
                    }
                },
                ClientCatalogs = new List<ClientCatalogDto>
                {
                    new ClientCatalogDto
                    {
                        Id = 1,
                Name = "Client Cat 1",
                InternalId = "CT1"
                    },
                    new ClientCatalogDto
                    {
                        Id = 2,
                Name = "Client Cat 2",
                InternalId = "CT2"
                    }
                },
                DocumentType = "User Guide",

                HtmlLink = "document1/html/index.htm",
                PdfLink = "document1/pdf/document1.pdf",
                WordLink = "document1/word/document1.doc",
                OtherLink = null,

                IsFitForClients = true,
                LatestTopicsUpdated = "This is the first version of the document."

            };

            DocumentDtoInternal d3 = new DocumentDtoInternal
            {
                Id = 2,
                Title = "Document 2",
                ShortDescription = "This is a test document.",

                Product = "Product 2",
                Version = "V2018.3",
                Authors = new List<AuthorDto>
                {
                    new AuthorDto {
                        Id = 3,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
                    }
                },
                ClientCatalogs = new List<ClientCatalogDto>
                {
                    new ClientCatalogDto
                    {
                        Id = 3,
                Name = "Client Cat 3",
                InternalId = "CT3"
                    }
                },
                DocumentType = "Release Notes",

                HtmlLink = "document2/html/index.htm",
                PdfLink = "document2/pdf/document2.pdf",
                WordLink = "document3/word/document2.doc",
                OtherLink = null,

                IsFitForClients = false,
                LatestTopicsUpdated = "This is the first version of the document."

            };

            DocumentDtoInternal d4 = new DocumentDtoInternal
            {
                Id = 1
            };

            Assert.True(d1.Equals(d2));
            Assert.True(d1.Equals(d2, true));
            Assert.False(d1.Equals(d3));
            Assert.False(d1.Equals(d3, true));
            Assert.True(d1.Equals(d4));
            Assert.False(d1.Equals(d4, true));
        }

        [Fact(DisplayName = "List<DocumentDtoInternal>.Sort() should sort Documents by LatestUpdate, Product, Version, Type and Title")]
        public void DocumentsSortReturnsListSortedProperly()
        {
            List<DocumentDtoInternal> Documents = new List<DocumentDtoInternal>
            {
                #region 0-11                
                #region 0-5
                #region 3-5
                new DocumentDtoInternal // 4
                {
                    Id = 1,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 1",
                    Version = "V2018.1",
                    DocumentType = "User Guide",
                    Title = "Getting started with Product 1"
                },
                new DocumentDtoInternal // 5
                {
                    Id = 2,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 1",
                    Version = "V2018.1",
                    DocumentType = "User Guide",
                    Title = "Reporting User Guide"
                },
                new DocumentDtoInternal // 3
                {
                    Id = 3,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 1",
                    Version = "V2018.1",
                    DocumentType = "Release Notes",
                    Title = "Product 1 V2018.1 Release Notes"
                },
                #endregion

#region 0-2
                new DocumentDtoInternal // 1
                {
                    Id = 4,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 1",
                    Version = "V2018.2",
                    DocumentType = "User Guide",
                    Title = "Getting started with Product 1"
                },
                new DocumentDtoInternal // 2
                {
                    Id = 5,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 1",
                    Version = "V2018.2",
                    DocumentType = "User Guide",
                    Title = "Reporting User Guide"
                },
                new DocumentDtoInternal // 0
                {
                    Id = 6,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 1",
                    Version = "V2018.2",
                    DocumentType = "Release Notes",
                    Title = "Product 1 V2018.2 Release Notes"
                },
                #endregion
                #endregion

                #region 6-11
                #region 9-11
                new DocumentDtoInternal // 10
                {
                    Id = 7,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 2",
                    Version = "V2018.1",
                    DocumentType = "User Guide",
                    Title = "Getting started with Product 2"
                },
                new DocumentDtoInternal // 11
                {
                    Id = 8,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 2",
                    Version = "V2018.1",
                    DocumentType = "User Guide",
                    Title = "Reporting User Guide"
                },
                new DocumentDtoInternal // 9
                {
                    Id = 9,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 2",
                    Version = "V2018.1",
                    DocumentType = "Release Notes",
                    Title = "Product 1 V2018.1 Release Notes"
                },
                #endregion
                #region 6-8
                new DocumentDtoInternal // 7
                {
                    Id = 10,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 2",
                    Version = "V2018.2",
                    DocumentType = "User Guide",
                    Title = "Getting started with Product 2"
                },
                new DocumentDtoInternal // 8
                {
                    Id = 11,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 2",
                    Version = "V2018.2",
                    DocumentType = "User Guide",
                    Title = "Reporting User Guide"
                },
                new DocumentDtoInternal // 6
                {
                    Id = 12,
                    LatestUpdate = DateTime.Today,
                    Product = "Product 2",
                    Version = "V2018.2",
                    DocumentType = "Release Notes",
                    Title = "Product 1 V2018.2 Release Notes"
                },
                #endregion
                #endregion
                #endregion
                #region 12-23
                #region 15-17
                new DocumentDtoInternal // 16
                {
                    Id = 13,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 3",
                    Version = "V2018.1",
                    DocumentType = "User Guide",
                    Title = "Getting started with Product 3"
                },
                new DocumentDtoInternal // 17
                {
                    Id = 14,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 3",
                    Version = "V2018.1",
                    DocumentType = "User Guide",
                    Title = "Reporting User Guide"
                },
                new DocumentDtoInternal // 15
                {
                    Id = 15,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 3",
                    Version = "V2018.1",
                    DocumentType = "Release Notes",
                    Title = "Product 3 V2018.1 Release Notes"
                },
                #endregion
                #region 12-14
                new DocumentDtoInternal // 13
                {
                    Id = 16,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 3",
                    Version = "V2018.2",
                    DocumentType = "User Guide",
                    Title = "Getting started with Product 3"
                },
                new DocumentDtoInternal // 14
                {
                    Id = 17,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 3",
                    Version = "V2018.2",
                    DocumentType = "User Guide",
                    Title = "Reporting User Guide"
                },
                new DocumentDtoInternal // 12
                {
                    Id = 18,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 3",
                    Version = "V2018.2",
                    DocumentType = "Release Notes",
                    Title = "Product 3 V2018.2 Release Notes"
                },
                #endregion
                #region 21-23
                new DocumentDtoInternal // 22
                {
                    Id = 19,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 4",
                    Version = "V2018.1",
                    DocumentType = "User Guide",
                    Title = "Getting started with Product 4"
                },
                new DocumentDtoInternal // 23
                {
                    Id = 20,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 4",
                    Version = "V2018.1",
                    DocumentType = "User Guide",
                    Title = "Reporting User Guide"
                },
                new DocumentDtoInternal // 21
                {
                    Id = 21,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 4",
                    Version = "V2018.1",
                    DocumentType = "Release Notes",
                    Title = "Product 4 V2018.1 Release Notes"
                },
                #endregion
                #region 18-20
                new DocumentDtoInternal // 19
                {
                    Id = 22,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 4",
                    Version = "V2018.2",
                    DocumentType = "User Guide",
                    Title = "Getting started with Product 4"
                },
                new DocumentDtoInternal // 20
                {
                    Id = 23,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 4",
                    Version = "V2018.2",
                    DocumentType = "User Guide",
                    Title = "Reporting User Guide"
                },
                new DocumentDtoInternal // 18
                {
                    Id = 24,
                    LatestUpdate = DateTime.Today.AddMonths(-1),
                    Product = "Product 4",
                    Version = "V2018.2",
                    DocumentType = "Release Notes",
                    Title = "Product 4 V2018.2 Release Notes"
                }
                #endregion
                #endregion
            };

            Documents.Sort();

            Assert.True(Documents[0].Id.Equals(6));
            Assert.True(Documents[1].Id.Equals(4));
            Assert.True(Documents[2].Id.Equals(5));
            Assert.True(Documents[3].Id.Equals(3));
            Assert.True(Documents[4].Id.Equals(1));
            Assert.True(Documents[5].Id.Equals(2));
            Assert.True(Documents[6].Id.Equals(12));
            Assert.True(Documents[7].Id.Equals(10));
            Assert.True(Documents[8].Id.Equals(11));
            Assert.True(Documents[9].Id.Equals(9));
            Assert.True(Documents[10].Id.Equals(7));
            Assert.True(Documents[11].Id.Equals(8));

            Assert.True(Documents[12].Id.Equals(18));
            Assert.True(Documents[13].Id.Equals(16));
            Assert.True(Documents[14].Id.Equals(17));
            Assert.True(Documents[15].Id.Equals(15));
            Assert.True(Documents[16].Id.Equals(13));
            Assert.True(Documents[17].Id.Equals(14));
            Assert.True(Documents[18].Id.Equals(24));
            Assert.True(Documents[19].Id.Equals(22));
            Assert.True(Documents[20].Id.Equals(23));
            Assert.True(Documents[21].Id.Equals(21));
            Assert.True(Documents[22].Id.Equals(19));
            Assert.True(Documents[23].Id.Equals(20));
        }

        #endregion

        #region Test Mapper

        [Fact(DisplayName = "Document is properly mapped to DocumentDtoInternal")]
        public void DocumentProperlyMappedToDocumentDtoInternal()
        {
            DocumentDtoInternal DocumentDtoInternal = new DocumentDtoInternal
            {
                Id = 1,
                Title = "Document 1",
                ShortDescription = "This is a test document.",

                Product = "Product 1",
                Version = "V2018.1",
                Authors = new List<AuthorDto>
                {
                    new AuthorDto
                    {
                        Id = 1,
                        Alias = "ALIAS",
                        AitName = "Tech",
                        FirstName = "Tech",
                        LastName = "Writer",
                        Email = "tech@writer.com",
                        IsFormerAuthor = false
                    },
                    new AuthorDto
                    {
                        Id = 2,
                        Alias = "SALIA",
                        AitName = "Writer",
                        FirstName = "Writer",
                        LastName = "Tech",
                        Email = "tech2@writer.com",
                        IsFormerAuthor = false
                    }
                },
                ClientCatalogs = new List<ClientCatalogDto>
                {
                    new ClientCatalogDto
                    {
                        Id = 1,
                        Name = "Client Cat 1",
                        InternalId = "CT1"
                    },
                    new ClientCatalogDto
                    {
                        Id = 2,
                        Name = "Client Cat 2",
                        InternalId = "CT2"
                    }
                },
                DocumentType = "User Guide",

                HtmlLink = "document1/html/index.htm",
                PdfLink = "document1/pdf/document1.pdf",
                WordLink = "document1/word/document1.doc",
                OtherLink = null,

                IsFitForClients = true,
                LatestTopicsUpdated = "This is the first version of the document."

            };

            Document document = new Document
            {
                Id = 1,
                AitId = 987456,
                Title = "Document 1",
                ShortDescription = "This is a test document.",

                ProductVersionId = 1,
                ProductVersion = new ProductVersion
                {
                    Id = 1,
                    Release = "V2018.1",
                    Product = new Product
                    {
                        Id = 1,
                        FullName = "Product 1",
                        Alias = "P1",
                        ShortName = "P1"
                    },
                    ProductId = 1,
                    EndOfSupport = DateTime.Today.AddYears(1)

                },
                DocumentAuthors = new List<DocumentAuthor>
                {
                    new DocumentAuthor {
                        AuthorId = 1,
                        Author = new Author
                        {
                            Id = 1,
                            Alias = "ALIAS",
                            AitName = "Tech",
                            FirstName = "Tech",
                            LastName = "Writer",
                            Email = "tech@writer.com",
                            IsFormerAuthor = false
                        }
                    },
                    new DocumentAuthor {

                        AuthorId = 2,
                        Author = new Author
                        {
                            Id = 2,
                            Alias = "SALIA",
                            AitName = "Writer",
                            FirstName = "Writer",
                            LastName = "Tech",
                            Email = "tech2@writer.com",
                            IsFormerAuthor = false
                        }
                    }
                },
                DocumentCatalogs = new List<DocumentCatalog>
                {
                    new DocumentCatalog
                    {
                        CatalogId = 1,
                        Catalog = new ClientCatalog
                        {
                            Id = 1,
                            Name = "Client Cat 1",
                            InternalId = "CT1"
                        }
                    },
                    new DocumentCatalog
                    {
                        CatalogId = 2,
                        Catalog = new ClientCatalog
                        {
                            Id = 2,
                            Name = "Client Cat 2",
                            InternalId = "CT2"
                        }
                    }
                },
                DocumentTypeId = 1,
                DocumentType = new DocumentType
                {
                    Id = 1,
                    DocumentCategory = DocumentCategory.FunctionalDocumentation,
                    FullName = "User Guide",
                    ShortName = "UG"
                },

                HtmlLink = "document1/html/index.htm",
                PdfLink = "document1/pdf/document1.pdf",
                WordLink = "document1/word/document1.doc",
                OtherLink = null,

                IsFitForClients = true,
                Updates = new List<DocumentUpdate>
                {
                    new DocumentUpdate
                    {
                        IsPublished = true,
                        LatestTopicsUpdated = "This is the first version of the document."
                    }
                }

            };

            DocumentDtoInternal DocumentDtoInternal2 = _mapper.Map<DocumentDtoInternal>(document);

            Assert.NotNull(DocumentDtoInternal2);
            Assert.True(DocumentDtoInternal.Equals(DocumentDtoInternal2));
            Assert.True(DocumentDtoInternal.Equals(DocumentDtoInternal2, true));
        }

        #endregion
    }
}
