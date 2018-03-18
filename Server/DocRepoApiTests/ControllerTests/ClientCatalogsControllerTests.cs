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
    public class ClientCatalogsControllerTests
    {
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        #region Generate Test Catalog Data
        private ClientCatalogDto GetTestClientCatalogDto(int i)
        {
            return new ClientCatalogDto
            {
                Id = i,
                Name = $"Catalog {i}",
                InternalId = $"C{i}"
            };
        }
        #endregion

        #region Test GET Methods

        // GET Methods
        [Fact(DisplayName = "GetClientCatalogs() should return a list of all ClientCatalogs")]
        public void GetClientCatalogsReturnsListOfAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                var result = controller.GetClientCatalogs();
                ClientCatalogDto p3 = GetTestClientCatalogDto(3);

                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<ClientCatalogDto>>(result);
                Assert.Equal(10, result.Count());
                ClientCatalogDto p = (ClientCatalogDto)result.Where(r => r.Id == 3).FirstOrDefault();
                Assert.True(p.Equals(p3));
                Assert.True(p.Equals(p3, true));

            }
        }

        [Fact(DisplayName = "GetClientCatalog(id) should return the ClientCatalog with the the ID")]
        public async void GetClientCatalogByIdReturnsSingleClientCatalog()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                var result = await controller.GetClientCatalog(3);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;

                ClientCatalogDto p3 = GetTestClientCatalogDto(3);

                Assert.NotNull(resultValue);
                Assert.IsType<ClientCatalogDto>(resultValue);
                ClientCatalogDto p = (ClientCatalogDto)resultValue;
                Assert.True(p.Equals(p3));
                Assert.True(p.Equals(p3, true));
            }
        }


        [Fact(DisplayName = "GetClientCatalog(wrongId) should return NotFound")]
        public async void GetClientCatalogWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                var result = await controller.GetClientCatalog(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact(DisplayName = "GetClientCatalog(id) with ModelStateError should return BadRequest")]
        public async void GetClientCatalogModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.GetClientCatalog(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion        

        #region Test POST Methods
        // POST Methods
        [Fact(DisplayName = "PostClientCatalog(ClientCatalog) should create a new ClientCatalog")]
        public async void PostClientCatalogCorrectDataCreatesClientCatalog()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                ClientCatalogDto p11 = GetTestClientCatalogDto(11);

                var result = await controller.PostClientCatalog(p11);

                Assert.NotNull(result);
                var resultValue = Assert.IsType<CreatedAtActionResult>(result);


            }
        }

        [Fact(DisplayName = "PostClientCatalog(ClientCatalog) with ModelStateError should return BadRequest")]
        public async void PostClientCatalogModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                ClientCatalogDto p11 = GetTestClientCatalogDto(11);

                var result = await controller.PostClientCatalog(p11);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        [Fact(DisplayName = "PostMultipleClientCatalogs(ClientCatalogList) should create multiple new ClientCatalogs")]
        public async void PostMultipleClientCatalogsCorrectDataCreatesClientCatalogs()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                IEnumerable<ClientCatalogDto> ClientCatalogList = Enumerable.Range(11, 10).Select(i => GetTestClientCatalogDto(i));

                var result = await controller.PostMultipleClientCatalogs(ClientCatalogList);

                Assert.NotNull(result);
                Assert.IsType<CreatedAtActionResult>(result);

            }
        }

        [Fact(DisplayName = "PostMultipleClientCatalogs(ClientCatalogList) with ModelStateError should return BadRequest")]
        public async void PostMultipleClientCatalogsModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                IEnumerable<ClientCatalogDto> ClientCatalogList = Enumerable.Range(11, 10).Select(i => GetTestClientCatalogDto(i));

                var result = await controller.PostMultipleClientCatalogs(ClientCatalogList);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion

        #region Test DELETE Methods
        // DELETE Methods
        [Fact(DisplayName = "DeleteClientCatalog(id) should remove the ClientCatalog from context")]
        public async void DeleteClientCatalogIdDeletesClientCatalog()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                var result = await controller.DeleteClientCatalog(3);
                ClientCatalogDto p3 = GetTestClientCatalogDto(3);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.Equal(p3, resultValue);

            }
        }

        [Fact(DisplayName = "DeleteClientCatalog(wrongId) should return NotFound")]
        public async void DeleteClientCatalogWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                var result = await controller.DeleteClientCatalog(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "DeleteClientCatalog(id) with ModelStateError should return BadRequest")]
        public async void DeleteClientCatalogModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ClientCatalogsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.DeleteClientCatalog(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion
    }
}
