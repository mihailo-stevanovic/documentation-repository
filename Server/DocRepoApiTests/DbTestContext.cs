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
        /// <summary>
        /// Generates a new InMemoryDbContext with data.
        /// </summary>
        /// <returns>Test database context with data.</returns>
        public static DocRepoContext GenerateContextWithData()
        {
            var options = new DbContextOptionsBuilder<DocRepoContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DocRepoContext(options);

            // Create and add test authors
            var authors = Enumerable.Range(1, 10)
                .Select(i => new Author
                {
                    Id = i,
                    Alias = $"AUTH{i}",
                    FirstName = $"First Name {i}",
                    LastName = $"Last Name {i}",
                    Email = $"dauthor{i}@domain.com",
                    AitName = $"user{i}",
                    IsFormerAuthor = (i % 2 == 0)
                });

            context.Authors.AddRange(authors);

            // Create and add test products
            var products = Enumerable.Range(1, 10)
                .Select(i => new Product
                {
                    Id = i,
                    FullName = $"My Product {i}",
                    ShortName = $"MP{i}",
                    Alias = $"Old Name {i}"
                });

            context.Products.AddRange(products);

            // Create and add test products
            

            for (int i = 0; i < 10; i++)
            {
                var productVersions = Enumerable.Range(1, 10)
                .Select(j => new ProductVersion
                {
                    Id = j + i * 10,
                    Release = $"V{j}",
                    ProductId = i + 1,
                    EndOfSupport = DateTime.Today.AddMonths(j)
                });

                context.ProductVersions.AddRange(productVersions);
            }                       

            context.SaveChanges();
            return context;
        }
    }
}
