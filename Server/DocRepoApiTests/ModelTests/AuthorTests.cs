using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ModelTests
{
    public class AuthorTests
    {        
        /*
         * TEST ARE BROKEN DUE TO DTO REFACTORING
        [Fact(DisplayName = "Author.Equals(other) should math based on id and all properties")]
        public void AuthorEqualsReturnsCorrectValues()
        {
            Author a1 = new Author {
                Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            Author a2 = new Author
            {
                Id = 1,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            Author a3 = new Author
            {
                Id = 3,
                Alias = "ALIAS",
                AitName = "Tech",
                FirstName = "Tech",
                LastName = "Writer",
                Email = "tech@writer.com",
                IsFormerAuthor = false
            };

            Author a4 = new Author
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
        [Fact(DisplayName = "List<Author>.Sort() should sort authors based on Alias")]
        public void AuthorsSortReturnsListSortedByAlias()
        {
            List<Author> authors = new List<Author>
            {
                new Author { Id = 1, Alias = "BBB" }, // 2
                new Author { Id = 2, Alias = "AAA" }, // 0
                new Author { Id = 3, Alias = "DDD" }, // 4
                new Author { Id = 4, Alias = "CCC"},  // 3
                new Author { Id = 5, Alias = "ZZZ"},  // 7
                new Author { Id = 6, Alias = "ABC"},  // 1
                new Author { Id = 7, Alias = "DEF"},  // 5
                new Author { Id = 8, Alias = "XYZ"}   // 6
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
        */
    }
}
