using DocRepoApi.Data;
using DocRepoApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocRepoApiTests
{
    public static class DbTestContext
    {
        public static DocRepoContext GenerateContextWithData()
        {
            var options = new DbContextOptionsBuilder<DocRepoContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DocRepoContext(options);

            // Create and add test authors
            var authors = Enumerable.Range(1, 10)
                .Select(i => new Author {
                    Id = i,
                    Alias = $"AUTH{i}",
                    FirstName = $"First Name {i}",
                    LastName = $"Last Name {i}",
                    Email = $"dauthor{i}@domain.com",
                    AitName = $"user{i}",
                    IsFormerAuthor = (i % 2 == 0)
                });

            context.Authors.AddRange(authors);

            context.SaveChanges();
            return context;
        }
    }
}
