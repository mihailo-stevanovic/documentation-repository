using AutoMapper;
using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ModelTests
{
    public class DocumentDtoTests
    {
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        #region Test Compare and Sort
        [Fact(DisplayName = "DocumentDto.Equals(other) should math based on id and all properties")]
        public void DocumentDtoEqualsReturnsCorrectValues()
        {
            DocumentDto d1 = new DocumentDto
            {
                Id = 1,
                AitId = 987456,                
                Title = "Document 1",
                ShortDescription = "This is a test document.",
                
                ProductVersionId = 1,
                DocumentAuthorIds = new List<int> { 1, 2 },
                DocumentCatalogIds = new List<int> { 1, 2 },
                DocumentTypeId = 1,

                HtmlLink = "document1/html/index.htm",
                PdfLink = "document1/pdf/document1.pdf",
                WordLink = "document1/word/document1.doc",
                OtherLink = null,

                IsFitForClients = true,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document."

            };

            DocumentDto d2 = new DocumentDto
            {
                Id = 1,
                AitId = 987456,
                Title = "Document 1",
                ShortDescription = "This is a test document.",

                ProductVersionId = 1,
                DocumentAuthorIds = new List<int> { 1, 2 },
                DocumentCatalogIds = new List<int> { 1, 2 },
                DocumentTypeId = 1,

                HtmlLink = "document1/html/index.htm",
                PdfLink = "document1/pdf/document1.pdf",
                WordLink = "document1/word/document1.doc",
                OtherLink = null,

                IsFitForClients = true,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document."

            };

            DocumentDto d3 = new DocumentDto
            {
                Id = 2,
                AitId = 987457,
                Title = "Document 2",
                ShortDescription = "This is a second test document.",

                ProductVersionId = 2,
                DocumentAuthorIds = new List<int> { 2 },
                DocumentCatalogIds = new List<int> { 1 },
                DocumentTypeId = 2,

                HtmlLink = "document2/html/index.htm",
                PdfLink = "document2/pdf/document2.pdf",
                WordLink = "document2/word/document2.doc",
                OtherLink = null,

                IsFitForClients = false,
                IsPublished = false,
                LatestTopicsUpdated = "This is the first version of the document."

            };
            DocumentDto d4 = new DocumentDto
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
        [Fact(DisplayName = "List<DocumentDto>.Sort() should sort Documents based on Alias")]
        public void DocumentsSortReturnsListSortedByAlias()
        {
            List<DocumentDto> Documents = new List<DocumentDto>
            {
                new DocumentDto { Id = 1, Title = "BBB", ProductVersionId = 6 },  // 2
                new DocumentDto { Id = 2, Title = "AAA", ProductVersionId = 7 },  // 0
                new DocumentDto { Id = 3, Title = "BBB", ProductVersionId = 4 },  // 4
                new DocumentDto { Id = 4, Title = "BBB", ProductVersionId = 5 },  // 3
                new DocumentDto { Id = 5, Title = "CCC", ProductVersionId = 1 },  // 7
                new DocumentDto { Id = 6, Title = "AAA", ProductVersionId = 6 },  // 1
                new DocumentDto { Id = 7, Title = "CCC", ProductVersionId = 3 },  // 5
                new DocumentDto { Id = 8, Title = "CCC", ProductVersionId = 2 }   // 6
            };

            Documents.Sort();

            Assert.True(Documents[0].Id.Equals(2));
            Assert.True(Documents[1].Id.Equals(6));
            Assert.True(Documents[2].Id.Equals(1));
            Assert.True(Documents[3].Id.Equals(4));
            Assert.True(Documents[4].Id.Equals(3));
            Assert.True(Documents[5].Id.Equals(7));
            Assert.True(Documents[6].Id.Equals(8));
            Assert.True(Documents[7].Id.Equals(5));

        }

        #endregion

        #region Test Mapper

        [Fact(DisplayName = "Document is properly mapped to DocumentDto")]
        public void DocumentProperlyMappedToDocumentDto()
        {
            DocumentDto documentDto = new DocumentDto
            {
                Id = 1,
                AitId = 987456,
                Title = "Document 1",
                ShortDescription = "This is a test document.",

                ProductVersionId = 1,
                DocumentAuthorIds = new List<int> { 1, 2 },
                DocumentCatalogIds = new List<int> { 1, 2 },
                DocumentTypeId = 1,

                HtmlLink = "document1/html/index.htm",
                PdfLink = "document1/pdf/document1.pdf",
                WordLink = "document1/word/document1.doc",
                OtherLink = null,

                IsFitForClients = true,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document."

            };

            Document document = new Document
            {
                Id = 1,
                AitId = 987456,
                Title = "Document 1",
                ShortDescription = "This is a test document.",

                ProductVersionId = 1,
                DocumentAuthors = new List<DocumentAuthor>
                {
                    new DocumentAuthor { AuthorId = 1 },
                    new DocumentAuthor { AuthorId = 2 }
                },
                DocumentCatalogs = new List<DocumentCatalog>
                {
                    new DocumentCatalog { CatalogId = 1 },
                    new DocumentCatalog { CatalogId = 2 }
                },
                DocumentTypeId = 1,

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

            DocumentDto documentDto2 = _mapper.Map<DocumentDto>(document);

            Assert.NotNull(documentDto2);
            Assert.True(documentDto.Equals(documentDto2));
            Assert.True(documentDto.Equals(documentDto2, true));
        }

        [Fact(DisplayName = "DocumentDto is properly mapped to Document")]
        public void DocumentDtoProperlyMappedToDocument()
        {           
            DocumentDto documentDto = new DocumentDto
            {
                Id = 1,
                AitId = 987456,
                Title = "Document 1",
                ShortDescription = "This is a test document.",

                ProductVersionId = 1,
                DocumentAuthorIds = new List<int> { 1, 2 },
                DocumentCatalogIds = new List<int> { 1, 2 },
                DocumentTypeId = 1,

                HtmlLink = "document1/html/index.htm",
                PdfLink = "document1/pdf/document1.pdf",
                WordLink = "document1/word/document1.doc",
                OtherLink = null,

                IsFitForClients = true,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document."

            };

            Document document = new Document
            {
                Id = 1,
                AitId = 987456,
                Title = "Document 1",
                ShortDescription = "This is a test document.",

                ProductVersionId = 1,
                DocumentAuthors = new List<DocumentAuthor>
                {
                    new DocumentAuthor { AuthorId = 1 },
                    new DocumentAuthor { AuthorId = 2 }
                },
                DocumentCatalogs = new List<DocumentCatalog>
                {
                    new DocumentCatalog { CatalogId = 1 },
                    new DocumentCatalog { CatalogId = 2 }
                },
                DocumentTypeId = 1,

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

            Document document2 = _mapper.Map<Document>(documentDto);            

            Assert.NotNull(document2);            

            Assert.True(document.Equals(document2));
            Assert.True(document.Equals(document2, true));
        }
        #endregion
    }
}
