using AutoMapper;
using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ModelTests
{
    public class DocumentUpdateDtoTests
    {
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        #region Test Compare and Sort
        [Fact(DisplayName = "DocumentUpdateDto.Equals(other) should math based on id and all properties")]
        public void DocumentUpdateEqualsReturnsCorrectValues()
        {
            DocumentUpdateDto up1 = new DocumentUpdateDto
            {
                Id = 1,
                DocumentId = 1,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document.",
                Timestamp = DateTime.Today
            };

            DocumentUpdateDto up2 = new DocumentUpdateDto
            {
                Id = 1,
                DocumentId = 1,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document.",
                Timestamp = DateTime.Today
            };

            DocumentUpdateDto up3 = new DocumentUpdateDto
            {
                Id = 3,
                DocumentId = 2,
                IsPublished = false,
                LatestTopicsUpdated = "This is the second version of the document.",
                Timestamp = DateTime.Today.AddDays(1)
            };

            DocumentUpdateDto up4 = new DocumentUpdateDto
            {
                Id = 1,
                DocumentId = 2,
                IsPublished = false,
                LatestTopicsUpdated = "This is the second version of the document.",
                Timestamp = DateTime.Today.AddDays(1)
            };

            Assert.True(up1.Equals(up2));
            Assert.True(up1.Equals(up2, true));
            Assert.False(up1.Equals(up3));
            Assert.False(up1.Equals(up3, true));
            Assert.True(up1.Equals(up4));
            Assert.False(up1.Equals(up4, true));

        }
        [Fact(DisplayName = "List<DocumentUpdateDto>.Sort() should sort DocumentUpdates based on Alias")]
        public void DocumentUpdatesSortReturnsListSortedByAlias()
        {
            List<DocumentUpdateDto> DocumentUpdates = new List<DocumentUpdateDto>
            {
                new DocumentUpdateDto { Id = 1, Timestamp = DateTime.UtcNow.AddMonths(1).AddDays(-1) }, // 2
                new DocumentUpdateDto { Id = 2, Timestamp = DateTime.UtcNow.AddMonths(1) }, // 0
                new DocumentUpdateDto { Id = 3, Timestamp = DateTime.UtcNow.AddDays(1) }, // 4
                new DocumentUpdateDto { Id = 4, Timestamp = DateTime.UtcNow.AddMonths(1).AddDays(-7) },  // 3
                new DocumentUpdateDto { Id = 5, Timestamp = DateTime.UtcNow },  // 7
                new DocumentUpdateDto { Id = 6, Timestamp = DateTime.UtcNow.AddMonths(1).AddHours(-1) },  // 1
                new DocumentUpdateDto { Id = 7, Timestamp = DateTime.UtcNow.AddMinutes(10) },  // 5
                new DocumentUpdateDto { Id = 8, Timestamp = DateTime.UtcNow }   // 6
            };

            DocumentUpdates.Sort();

            Assert.True(DocumentUpdates[0].Id.Equals(2));
            Assert.True(DocumentUpdates[1].Id.Equals(6));
            Assert.True(DocumentUpdates[2].Id.Equals(1));
            Assert.True(DocumentUpdates[3].Id.Equals(4));
            Assert.True(DocumentUpdates[4].Id.Equals(3));
            Assert.True(DocumentUpdates[5].Id.Equals(7));
            Assert.True(DocumentUpdates[6].Id.Equals(8));
            Assert.True(DocumentUpdates[7].Id.Equals(5));

        }
        #endregion

        #region Test Mapping
        [Fact(DisplayName = "DocumentUpdate is properly mapped to DocumentUpdateDto")]
        public void DocumentUpdateProperlyMappedToDocumentUpdateDto()
        {
            DocumentUpdateDto upDto1 = new DocumentUpdateDto
            {
                Id = 1,
                DocumentId = 1,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document.",
                Timestamp = DateTime.Today
            };

            DocumentUpdate up1 = new DocumentUpdate
            {
                Id = 1,
                DocumentId = 1,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document.",
                Timestamp = DateTime.Today
            };

            DocumentUpdateDto aDto2 = _mapper.Map<DocumentUpdateDto>(up1);

            Assert.NotNull(aDto2);
            Assert.True(upDto1.Equals(aDto2));
            Assert.True(upDto1.Equals(aDto2, true));
        }

        [Fact(DisplayName = "DocumentUpdateDto is properly reversed to DocumentUpdate")]
        public void DocumentUpdateDtoProperlyReversedToDocumentUpdate()
        {
            DocumentUpdateDto upDto1 = new DocumentUpdateDto
            {
                Id = 1,
                DocumentId = 1,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document.",
                Timestamp = DateTime.Today
            };

            DocumentUpdate up1 = new DocumentUpdate
            {
                Id = 1,
                DocumentId = 1,
                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document.",
                Timestamp = DateTime.Today
            };

            DocumentUpdate a2 = _mapper.Map<DocumentUpdate>(upDto1);

            Assert.NotNull(a2);
            Assert.True(up1.Equals(a2));
            Assert.True(up1.Equals(a2, true));
        }
        #endregion
    }
}
