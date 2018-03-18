using AutoMapper;
using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ModelTests
{
    public class DocumentTypeDtoTests
    {
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        #region Test Compare and Sort
        [Fact(DisplayName = "DocumentTypeDto.Equals(other) should match based on id and all properties")]
        public void DocumentTypeDtoEqualsRetursCorrectValues()
        {
            DocumentTypeDto d1 = new DocumentTypeDto
            {
                Id = 1,
                FullName = "Doc Type 1",
                ShortName = "DT1",
                DocumentCategory = DocumentCategory.FunctionalDocumentation
            };
            DocumentTypeDto d2 = new DocumentTypeDto
            {
                Id = 1,
                FullName = "Doc Type 1",
                ShortName = "DT1",
                DocumentCategory = DocumentCategory.FunctionalDocumentation
            };
            DocumentTypeDto d3 = new DocumentTypeDto
            {
                Id = 3,
                FullName = "Doc Type 1",
                ShortName = "DT1",
                DocumentCategory = DocumentCategory.FunctionalDocumentation
            };
            DocumentTypeDto d4 = new DocumentTypeDto
            {
                Id = 1,
                FullName = "Doc Type 4",
                ShortName = "DT4",
                DocumentCategory = DocumentCategory.ReleaseNotes
            };

            Assert.True(d1.Equals(d2));
            Assert.True(d1.Equals(d2, true));
            Assert.False(d1.Equals(d3));
            Assert.False(d1.Equals(d3, true));
            Assert.True(d1.Equals(d4));
            Assert.False(d1.Equals(d4, true));
        }
        
        [Fact(DisplayName = "List<DocumentTypeDto>.Sort() should sort authors based on Category then FullName")]
        public void DocumentTypeDtoSortReturnsListSortedByFullName()
        {
            List<DocumentTypeDto> DocumentTypes = new List<DocumentTypeDto>
            {
                new DocumentTypeDto { Id = 1, FullName = "BBB", DocumentCategory = DocumentCategory.TechnicalDocumentation  }, // 2
                new DocumentTypeDto { Id = 2, FullName = "AAA", DocumentCategory = DocumentCategory.FunctionalDocumentation }, // 0
                new DocumentTypeDto { Id = 3, FullName = "DDD", DocumentCategory = DocumentCategory.TechnicalDocumentation }, // 4
                new DocumentTypeDto { Id = 4, FullName = "CCC", DocumentCategory = DocumentCategory.TechnicalDocumentation },  // 3
                new DocumentTypeDto { Id = 5, FullName = "MHG", DocumentCategory = DocumentCategory.Other },  // 7
                new DocumentTypeDto { Id = 6, FullName = "ZZZ", DocumentCategory = DocumentCategory.FunctionalDocumentation },  // 1
                new DocumentTypeDto { Id = 7, FullName = "DEF", DocumentCategory = DocumentCategory.ReleaseNotes},  // 5
                new DocumentTypeDto { Id = 8, FullName = "XYZ", DocumentCategory = DocumentCategory.ReleaseNotes}   // 6
            };

            DocumentTypes.Sort();

            Assert.True(DocumentTypes[0].Id.Equals(2));
            Assert.True(DocumentTypes[1].Id.Equals(6));
            Assert.True(DocumentTypes[2].Id.Equals(1));
            Assert.True(DocumentTypes[3].Id.Equals(4));
            Assert.True(DocumentTypes[4].Id.Equals(3));
            Assert.True(DocumentTypes[5].Id.Equals(7));
            Assert.True(DocumentTypes[6].Id.Equals(8));
            Assert.True(DocumentTypes[7].Id.Equals(5));

        }
        #endregion

        #region Test Mapping
        [Fact(DisplayName = "DocumentType is properly mapped to DocumentTypeDto")]
        public void DocumentTypeProperlyMappedToDocumentTypeDto()
        {
            DocumentTypeDto pDto1 = new DocumentTypeDto
            {
                Id = 1,
                FullName = "Doc Type 1",
                ShortName = "DT1",
                DocumentCategory = DocumentCategory.FunctionalDocumentation
            };
            DocumentType p1 = new DocumentType
            {
                Id = 1,
                FullName = "Doc Type 1",
                ShortName = "DT1",
                DocumentCategory = DocumentCategory.FunctionalDocumentation
            };

            DocumentTypeDto pDto2 = _mapper.Map<DocumentTypeDto>(p1);

            Assert.NotNull(pDto2);
            Assert.True(pDto1.Equals(pDto2));
            Assert.True(pDto1.Equals(pDto2, true));
        }

        [Fact(DisplayName = "DocumentTypeDto is properly reversed to DocumentType")]
        public void DocumentTypeDtoProperlyReversedToDocumentType()
        {
            DocumentTypeDto pDto1 = new DocumentTypeDto
            {
                Id = 1,
                FullName = "Doc Type 1",
                ShortName = "DT1",
                DocumentCategory = DocumentCategory.FunctionalDocumentation
            };
            DocumentType p1 = new DocumentType
            {
                Id = 1,
                FullName = "Doc Type 1",
                ShortName = "DT1",
                DocumentCategory = DocumentCategory.FunctionalDocumentation
            };

            DocumentType p2 = _mapper.Map<DocumentType>(pDto1);

            Assert.NotNull(p2);
            Assert.True(p1.Equals(p2));
            Assert.True(p1.Equals(p2, true));
        }
        #endregion
    }
}
