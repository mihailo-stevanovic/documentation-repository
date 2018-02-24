using DocRepoApi.Controllers;
using DocRepoApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ControllerTests
{
    public class AuthorsControllerTests
    {
        // GET Methods

        [Fact(DisplayName = "GetAuthors() should return a list of all Authors")]
        public void GetAuthorsReturnsListOfAuthors()
        {            
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context))
            {
                var result = controller.GetAuthors();

                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<Author>>(result);
                Assert.Equal(10, result.Count());
                Assert.Equal("AUTH3", result.Where(r => r.Id == 3).Select(r => r.Alias).First());
                Assert.NotEmpty(result.Where(r => r.DocumentsAuthored != null));
            }
        }

        [Fact(DisplayName = "GetAuthors(false) should return a list of all Authors without documents")]
        public void GetAuthorsReturnsListOfAuthorsWithoutDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context))
            {
                var result = controller.GetAuthors(false);

                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<Author>>(result);
                Assert.Equal(10, result.Count());
                Assert.Empty(result.Where(r => r.DocumentsAuthored != null));
            }
        }

        [Fact(DisplayName = "GetActiveAuthors() should return a list of Authors with IsFormerAuthor=false")]
        public void GetActiveAuthorsReturnsListOfNotFormerAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context))
            {
                var result = controller.GetActiveAuthors();

                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<Author>>(result);
                Assert.Equal(5, result.Count());
                Assert.Equal("AUTH3", result.Where(r => r.Id == 3).Select(r => r.Alias).First());
                Assert.NotEmpty(result.Where(r => r.DocumentsAuthored != null));
            }
        }

        [Fact(DisplayName = "GetActiveAuthors(false) should return a list of Authors with IsFormerAuthor=false without documents")]
        public void GetActiveAuthorsReturnsListOfNotFormerAuthorsWithoutDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context))
            {
                var result = controller.GetActiveAuthors(false);

                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<Author>>(result);
                Assert.Equal(5, result.Count());
                Assert.Equal("AUTH3", result.Where(r => r.Id == 3).Select(r => r.Alias).First());
                Assert.Empty(result.Where(r => r.DocumentsAuthored != null));
            }
        }

        [Fact(DisplayName = "GetAuthor(id) should return the Author with the the ID")]
        public async void GetAuthorByIdReturnsSingleAuthor()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context))
            {
                var result = await controller.GetAuthor(3);

                var okObjectResult = Assert.IsType<OkObjectResult>(result);

                var resultValue = okObjectResult.Value;

                Assert.NotNull(resultValue);
                Assert.IsType<Author>(resultValue);
                Assert.Equal("AUTH3", ((Author)resultValue).Alias );
                Assert.NotNull(((Author)resultValue).DocumentsAuthored);
            }
        }

        [Fact(DisplayName = "GetAuthor(id, false) should return the Author with the the ID but without documents")]
        public async void GetAuthorByIdFalseReturnsSingleAuthorWithoutDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context))
            {
                var result = await controller.GetAuthor(3, false);

                var okObjectResult = Assert.IsType<OkObjectResult>(result);

                var resultValue = okObjectResult.Value;

                Assert.NotNull(resultValue);
                Assert.IsType<Author>(resultValue);
                Assert.Equal("AUTH3", ((Author)resultValue).Alias);
                Assert.Null(((Author)resultValue).DocumentsAuthored);
            }
        }

        [Fact(DisplayName = "GetAuthor(wrongId) should return NotFound")]
        public async void GetAuthorWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context))
            {
                var result = await controller.GetAuthor(99);

                Assert.IsType<NotFoundResult>(result);
                
            }
        }

        [Fact(DisplayName = "GetAuthor(id) with ModelStateError should return BadRequest")]
        public async void GetAuthorModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.GetAuthor(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

    }
}