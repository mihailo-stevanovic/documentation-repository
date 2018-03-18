using AutoMapper;
using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ModelTests
{
    public class AuthorDtoTests
    {

        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        [Fact(DisplayName = "AuthorDto.Equals(other) should math based on id and all properties")]
        public void AuthorEqualsReturnsCorrectValues()
        {
            AuthorDto a1 = new AuthorDto
            {
                Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            AuthorDto a2 = new AuthorDto
            {
                Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            AuthorDto a3 = new AuthorDto
            {
                Id = 3,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            AuthorDto a4 = new AuthorDto
            {
                Id = 1,
                Alias = "SAILA",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            Assert.True(a1.Equals(a2));
            Assert.True(a1.Equals(a2, true));
            Assert.False(a1.Equals(a3));
            Assert.False(a1.Equals(a3, true));
            Assert.True(a1.Equals(a4));
            Assert.False(a1.Equals(a4, true));

        }
        [Fact(DisplayName = "List<AuthorDto>.Sort() should sort authors based on Alias")]
        public void AuthorsSortReturnsListSortedByAlias()
        {
            List<AuthorDto> authors = new List<AuthorDto>
            {
                new AuthorDto { Id = 1, Alias = "BBB" }, // 2
                new AuthorDto { Id = 2, Alias = "AAA" }, // 0
                new AuthorDto { Id = 3, Alias = "DDD" }, // 4
                new AuthorDto { Id = 4, Alias = "CCC"},  // 3
                new AuthorDto { Id = 5, Alias = "ZZZ"},  // 7
                new AuthorDto { Id = 6, Alias = "ABC"},  // 1
                new AuthorDto { Id = 7, Alias = "DEF"},  // 5
                new AuthorDto { Id = 8, Alias = "XYZ"}   // 6
            };

            authors.Sort();

            Assert.True(authors[0].Id.Equals(2));
            Assert.True(authors[1].Id.Equals(6));
            Assert.True(authors[2].Id.Equals(1));
            Assert.True(authors[3].Id.Equals(4));
            Assert.True(authors[4].Id.Equals(3));
            Assert.True(authors[5].Id.Equals(7));
            Assert.True(authors[6].Id.Equals(8));
            Assert.True(authors[7].Id.Equals(5));

        }

        [Fact(DisplayName = "Author is properly mapped to AuthorDto")]
        public void AuthorProperlyMappedToAuthorDto()
        {
            AuthorDto aDto1 = new AuthorDto
            {
                Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            Author a1 = new Author
            {
                Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            AuthorDto aDto2 = _mapper.Map<AuthorDto>(a1);

            Assert.NotNull(aDto2);
            Assert.True(aDto1.Equals(aDto2));
            Assert.True(aDto1.Equals(aDto2, true));
        }

        [Fact(DisplayName = "AuthorDto is properly reversed to Author")]
        public void AuthorDtoProperlyReversedToAuthor()
        {
            AuthorDto aDto1 = new AuthorDto
            {
                Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            Author a1 = new Author
            {
                Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            Author a2 = _mapper.Map<Author>(aDto1);

            Assert.NotNull(a2);
            Assert.True(a1.Equals(a2));
            Assert.True(a1.Equals(a2, true));
        }

    }
}
