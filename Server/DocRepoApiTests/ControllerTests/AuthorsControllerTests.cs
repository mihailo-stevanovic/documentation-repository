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

        // GET Methods

        [Fact(DisplayName = "GetAuthors() should return a list of all Authors")]
        public void GetAuthorsReturnsListOfAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = controller.GetAuthors();
                AuthorDto a3 = GetTestAuthorDto(3);

                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<AuthorDto>>(result);
                Assert.Equal(10, result.Count());
                AuthorDto a = (AuthorDto)result.Where(r => r.Id == 3).FirstOrDefault();
                Assert.True(a.Equals(a3));
                Assert.True(a.Equals(a3, true));

            }
        }



        [Fact(DisplayName = "GetActiveAuthors() should return a list of Authors with IsFormerAuthor=false")]
        public void GetActiveAuthorsReturnsListOfNotFormerAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new AuthorsController(context, _mapper))
            {
                var result = controller.GetActiveAuthors();
                AuthorDto a3 = GetTestAuthorDto(3);

                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<AuthorDto>>(result);
                Assert.Equal(5, result.Count());
                AuthorDto a = (AuthorDto)result.Where(r => r.Id == 3).FirstOrDefault();
                Assert.True(a.Equals(a3));
                Assert.True(a.Equals(a3, true));
            }
        }


        [Fact(DisplayName = "GetAuthor(id) should return the Author with the the ID")]
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

        /*
         * Cannot unit test with InMemoryDB, should be included in integration tests
         * 
         // PUT Methods        
         [Fact(DisplayName = "PutAuthor(id, Author) should update the context")]
         public async void PutAuthorCorrectDataUpdatesContext()
         {           

             using (var context = DbTestContext.GenerateContextWithData())
             using (var controller = new AuthorsController(context, _mapper))
             {
                 int id = 3;
                 Author a3 = context.Authors.Find(id);
                 AuthorDto a3Dto = _mapper.Map<AuthorDto>(a3);

                 a3Dto.LastName = "Modified";                

                 var result = await controller.PutAuthor(id, a3Dto);

                 Assert.IsType<NoContentResult>(result);         


             }
         }
         */

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

        [Fact(DisplayName = "PostMultipleAuthors(AuthorList) should create a muultiple new Authors")]
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
        [Fact(DisplayName = "DeleteAuthor(id) should remove the author from context")]
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