using AutoMapper;
using DocRepoApi.Controllers;
using DocRepoApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DocRepoApiTests.ControllerTests
{
    public class AuthorsControllerTests
    {
        /// <summary>
        /// Mapper test context.
        /// </summary>
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();
        #region Generate Test Author Data
        /// <summary>
        /// Create a single author for testing.
        /// </summary>
        /// <param name="i">ID of the test author.</param>
        /// <returns>A single test author.</returns>
        private Author GetTestAuthor(int i)
        {
            return new Author
            {
                Id = i,
                Alias = $"AUTH{i}",
                FirstName = $"First Name {i}",
                LastName = $"Last Name {i}",
                Email = $"dauthor{i}@domain.com",
                AitName = $"user{i}",
                IsFormerAuthor = (i % 2 == 0)
            };
        }
        /// <summary>
        /// Create a single author DTO for testing.
        /// </summary>
        /// <param name="i">ID of the test author.</param>
        /// <returns>A single author DTO for testing.</returns>
        private AuthorDto GetTestAuthorDto(int i, bool usingMapping = false)
        {
            if (usingMapping)
            {
                return _mapper.Map<AuthorDto>(GetTestAuthor(i));
            }

            return new AuthorDto
            {
                Id = i,
                Alias = $"AUTH{i}",
                FirstName = $"First Name {i}",
                LastName = $"Last Name {i}",
                Email = $"dauthor{i}@domain.com",
                AitName = $"user{i}",
                IsFormerAuthor = (i % 2 == 0)
            };

        }
        /// <summary>
        /// Returns AuthorDto with the specified ID from the test database.
        /// </summary>
        /// <param name="id">ID of the author.</param>
        /// <returns>Auhtor Dto.</returns>
        private AuthorDto GetTestAuthorFromDb(int id)
        {
            var dbTestContext = DbTestContext.GenerateContextWithData();
            var author = dbTestContext.Authors                
                .Single(a => a.Id == id);

            return _mapper.Map<AuthorDto>(author);
        }
       

        private AuthorDto[] GetAuthorDtoArray()
        {
            AuthorDto[] authorDtoArray = new AuthorDto[10];

            for (int i = 0; i < 10; i++)
            {
                authorDtoArray[i] = GetTestAuthorDto(i + 11);
            }

            return authorDtoArray;
        }

        #endregion

        #region Test GET Methods

        #region GET: api/v1/Authors
        [Fact(DisplayName = "GetAuthors() should return a list of all Authors")]
        public void GetAuthorsReturnsListOfAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = controller.GetAuthors();
                AuthorDto a3 = GetTestAuthorDto(3);

                Assert.NotNull(result);
                
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<AuthorDto>>(resultValue);
                Assert.NotEmpty((IEnumerable<AuthorDto>)resultValue);

                IEnumerable<AuthorDto> resultValueList = (IEnumerable<AuthorDto>)resultValue;

                Assert.Equal(10, resultValueList.Count());
                AuthorDto a = (AuthorDto)resultValueList.Where(r => r.Id == 3).FirstOrDefault();
                Assert.True(a.Equals(a3));
                Assert.True(a.Equals(a3, true));                

            }
        }

        [Fact(DisplayName = "GetAuthors() should return NotFound if context is empty")]
        public void GetAuthorsEmptyContextNotFound()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = controller.GetAuthors();

                Assert.IsType<NotFoundResult>(result);

            }
        }
        #endregion
        #region GET: api/v1/Authors/Active
        [Fact(DisplayName = "GetActiveAuthors() should return a list of Authors with IsFormerAuthor=false")]
        public void GetActiveAuthorsReturnsListOfNotFormerAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = controller.GetActiveAuthors();
                AuthorDto a3 = GetTestAuthorDto(3);

                Assert.NotNull(result);

                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<AuthorDto>>(resultValue);
                Assert.NotEmpty((IEnumerable<AuthorDto>)resultValue);

                IEnumerable<AuthorDto> resultValueList = (IEnumerable<AuthorDto>)resultValue;

                Assert.Equal(5, resultValueList.Count());
                AuthorDto a = (AuthorDto)resultValueList.Where(r => r.Id == 3).FirstOrDefault();
                Assert.True(a.Equals(a3));
                Assert.True(a.Equals(a3, true));
            }
        }

        [Fact(DisplayName = "GetActiveAuthors() should return NotFound if context is empty")]
        public void GetActiveAuthorsEmptyContextNotFound()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = controller.GetActiveAuthors();

                Assert.IsType<NotFoundResult>(result);

            }
        }
        #endregion
        #region GET: api/v1/Authors/5
        [Fact(DisplayName = "GetAuthor(id) should return the correct Author")]
        public async void GetAuthorByIdReturnsSingleAuthor()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = await controller.GetAuthor(3);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;

                AuthorDto a3 = GetTestAuthorDto(3);

                Assert.NotNull(resultValue);
                Assert.IsType<AuthorDto>(resultValue);
                AuthorDto a = (AuthorDto)resultValue;
                Assert.True(a.Equals(a3));
                Assert.True(a.Equals(a3, true));
            }
        }

        [Fact(DisplayName = "GetAuthor(wrongId) should return NotFound")]
        public async void GetAuthorWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = await controller.GetAuthor(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetAuthor(id) with ModelStateError should return BadRequest")]
        public async void GetAuthorModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.GetAuthor(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion
        
        #endregion       

        #region Test POST Methods
        // POST Methods
        [Fact(DisplayName = "PostAuthor(Author) should create a new Author")]
        public async void PostAuthorCorrectDataCreatesAuthor()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                AuthorDto a11 = GetTestAuthorDto(11);

                var result = await controller.PostAuthor(a11);

                Assert.NotNull(result);
                var resultValue = Assert.IsType<CreatedAtActionResult>(result);


            }
        }

        [Fact(DisplayName = "PostAuthor(Author) with ModelStateError should return BadRequest")]
        public async void PostAuthorModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                AuthorDto a11 = GetTestAuthorDto(11);

                var result = await controller.PostAuthor(a11);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }        

        [Fact(DisplayName = "PostMultipleAuthors(AuthorList) should create multiple new Authors")]
        public async void PostMultipleAuthorsCorrectDataCreatesAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                IEnumerable<AuthorDto> authorList = Enumerable.Range(11, 10).Select(i => GetTestAuthorDto(i));

                var result = await controller.PostMultipleAuthors(authorList);

                Assert.NotNull(result);
                Assert.IsType<CreatedAtActionResult>(result);


            }
        }

        [Fact(DisplayName = "PostMultipleAuthors(AuthorList) with ModelStateError should return BadRequest")]
        public async void PostMultipleAuthorsModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                IEnumerable<AuthorDto> authorList = Enumerable.Range(11, 10).Select(i => GetTestAuthorDto(i));

                var result = await controller.PostMultipleAuthors(authorList);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region Test DELETE Methods

        // DELETE Methods
        [Fact(DisplayName = "DeleteAuthor(id) should remove the Author from context")]
        public async void DeleteAuthorIdDeletesAuthor()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = await controller.DeleteAuthor(3);
                AuthorDto a3 = GetTestAuthorDto(3);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.Equal(a3, resultValue);

            }
        }

        [Fact(DisplayName = "DeleteAuthor(wrongId) should return NotFound")]
        public async void DeleteAuthorWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = await controller.DeleteAuthor(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "DeleteAuthor(id) with ModelStateError should return BadRequest")]
        public async void DeleteAuthorModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.DeleteAuthor(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion
    }
}