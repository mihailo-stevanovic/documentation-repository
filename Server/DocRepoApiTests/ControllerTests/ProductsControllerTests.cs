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
    public class ProductsControllerTests
    {
        /// <summary>
        /// Mapper test context.
        /// </summary>
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        #region Generate Test Product Data
        /// <summary>
        /// Creates a new ProductDto object for testing.
        /// </summary>
        /// <param name="i">Integer used in the object properties.</param>
        /// <returns>A single test ProductDto</returns>
        private ProductDto GetTestProductDto(int i)
        {
            return new ProductDto
            {
                Id = i,
                FullName = $"My Product {i}",
                ShortName = $"MP{i}",
                Alias = $"Old Name {i}"
            };
        }
        private ProductDto[] GetProductDtoArray()
        {
            ProductDto[] ProductDtoArray = new ProductDto[10];

            for (int i = 0; i < 10; i++)
            {
                ProductDtoArray[i] = GetTestProductDto(i + 11);
            }

            return ProductDtoArray;
        }
        #endregion

        #region Generate Test Product Version Data
        /// <summary>
        /// Creates a new ProductVersionDto object for testing.
        /// </summary>
        /// <param name="j">Integer used in the object properties.</param>
        /// <param name="i">ProductID - 1</param>
        /// <returns>A single test ProductVersionDto</returns>
        private ProductVersionDto GetTestProductVersionDto(int j, int i = 0)
        {
            return new ProductVersionDto
            {
                Id = j + i * 10,
                Release = $"V{j}",
                Product = $"My Product {i + 1}",
                EndOfSupport = DateTime.Today.AddMonths(j)
            };
        }
        private ProductVersionDto[] GetProductVersionDtoArray()
        {
            ProductVersionDto[] ProductVersionDtoArray = new ProductVersionDto[10];

            for (int i = 0; i < 10; i++)
            {
                ProductVersionDtoArray[i] = GetTestProductVersionDto(i + 11);
            }

            return ProductVersionDtoArray;
        }
        #endregion

        #region Test Product Methods

        #region Test GET Methods

        // GET Methods
        [Fact(DisplayName = "GetProducts() should return a list of all Products")]
        public void GetProductsReturnsListOfAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = controller.GetProducts();
                ProductDto p3 = GetTestProductDto(3);

                Assert.NotNull(result);

                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<ProductDto>>(resultValue);
                Assert.NotEmpty((IEnumerable<ProductDto>)resultValue);

                IEnumerable<ProductDto> resultValueList = (IEnumerable<ProductDto>)resultValue;

                Assert.Equal(10, resultValueList.Count());
                ProductDto p = (ProductDto)resultValueList.Where(r => r.Id == 3).FirstOrDefault();
                Assert.True(p.Equals(p3));
                Assert.True(p.Equals(p3, true));

            }
        }

        [Fact(DisplayName = "GetProducts() should return NotFound if context is empty")]
        public void GetProductsEmptyContextNotFound()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = controller.GetProducts();

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetProduct(id) should return the Product")]
        public async void GetProductByIdReturnsSingleProduct()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = await controller.GetProduct(3);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;

                ProductDto p3 = GetTestProductDto(3);

                Assert.NotNull(resultValue);
                Assert.IsType<ProductDto>(resultValue);
                ProductDto p = (ProductDto)resultValue;
                Assert.True(p.Equals(p3));
                Assert.True(p.Equals(p3, true));
            }
        }

        
        [Fact(DisplayName = "GetProduct(wrongId) should return NotFound")]
        public async void GetProductWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = await controller.GetProduct(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact(DisplayName = "GetProduct(id) with ModelStateError should return BadRequest")]
        public async void GetProductModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.GetProduct(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion        

        #region Test POST Methods
        // POST Methods
        [Fact(DisplayName = "PostProduct(Product) should create a new Product")]
        public async void PostProductCorrectDataCreatesProduct()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                ProductDto p11 = GetTestProductDto(11);

                var result = await controller.PostProduct(p11);

                Assert.NotNull(result);
                var resultValue = Assert.IsType<CreatedAtActionResult>(result);


            }
        }

        [Fact(DisplayName = "PostProduct(Product) with ModelStateError should return BadRequest")]
        public async void PostProductModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                ProductDto p11 = GetTestProductDto(11);

                var result = await controller.PostProduct(p11);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }        

        [Fact(DisplayName = "PostMultipleProducts(ProductList) should create multiple new Products")]
        public async void PostMultipleProductsCorrectDataCreatesProducts()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                IEnumerable<ProductDto> ProductList = Enumerable.Range(11, 10).Select(i => GetTestProductDto(i));

                var result = await controller.PostMultipleProducts(ProductList);

                Assert.NotNull(result);
                Assert.IsType<CreatedAtActionResult>(result);

            }
        }

        [Fact(DisplayName = "PostMultipleProducts(ProductList) with ModelStateError should return BadRequest")]
        public async void PostMultipleProductsModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                IEnumerable<ProductDto> ProductList = Enumerable.Range(11, 10).Select(i => GetTestProductDto(i));

                var result = await controller.PostMultipleProducts(ProductList);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion

        #region Test DELETE Methods
        // DELETE Methods
        [Fact(DisplayName = "DeleteProduct(id) should remove the Product from context")]
        public async void DeleteProductIdDeletesProduct()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = await controller.DeleteProduct(3);
                ProductDto p3 = GetTestProductDto(3);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.Equal(p3, resultValue);

            }
        }

        [Fact(DisplayName = "DeleteProduct(wrongId) should return NotFound")]
        public async void DeleteProductWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = await controller.DeleteProduct(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "DeleteProduct(id) with ModelStateError should return BadRequest")]
        public async void DeleteProductModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.DeleteProduct(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion

        #endregion

        #region Test Product Version Methods

        #region Test GET Methods
        
        // Get Methods
        [Fact(DisplayName = "GetProductVersions(productId) should return a list of all ProductVersions related to the Product")]
        public async void GetProductVersionsReturnsListOfAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = await controller.GetProductVersions(1);
                ProductVersionDto p3 = GetTestProductVersionDto(3);

                Assert.NotNull(result);

                var okObjectResult = Assert.IsType<OkObjectResult>(result);                
                Assert.IsAssignableFrom<IEnumerable<ProductVersionDto>>(okObjectResult.Value);
                IEnumerable<ProductVersionDto> resultValue = (IEnumerable<ProductVersionDto>)okObjectResult.Value;
                Assert.Equal(10, resultValue.Count());
                ProductVersionDto p = (ProductVersionDto)resultValue.Where(r => r.Id == 3).FirstOrDefault();
                Assert.True(p.Equals(p3));
                Assert.True(p.Equals(p3, true));

            }
        }
        
        [Fact(DisplayName = "GetProductVersion(productId,versionId) should return the ProductVersion")]
        public async void GetProductVersionByIdReturnsSingleProductVersion()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = await controller.GetProductVersion(1, 3);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;

                ProductVersionDto p3 = GetTestProductVersionDto(3);

                Assert.NotNull(resultValue);
                Assert.IsType<ProductVersionDto>(resultValue);
                ProductVersionDto p = (ProductVersionDto)resultValue;
                Assert.True(p.Equals(p3));
                Assert.True(p.Equals(p3, true));
            }
        }


        [Fact(DisplayName = "GetProductVersion(productId, wrongId) should return NotFound")]
        public async void GetProductVersionWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = await controller.GetProductVersion(1, 999);

                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact(DisplayName = "GetProductVersion(productId) with ModelStateError should return BadRequest")]
        public async void GetProductVersionModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.GetProductVersion(1, 1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region Test POST Methods

        // POST Methods
        [Fact(DisplayName = "PostProductVersion(ProductVersion) should create a new ProductVersion")]
        public async void PostProductVersionCorrectDataCreatesProductVersion()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                ProductVersionDto p11 = GetTestProductVersionDto(11, 9);

                var result = await controller.PostProductVersion(1, p11);

                Assert.NotNull(result);
                var resultValue = Assert.IsType<CreatedAtActionResult>(result);


            }
        }

        [Fact(DisplayName = "PostProductVersion(ProductVersion) with ModelStateError should return BadRequest")]
        public async void PostProductVersionModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                ProductVersionDto p11 = GetTestProductVersionDto(11);

                var result = await controller.PostProductVersion(1, p11);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        
        [Fact(DisplayName = "PostMultipleProductVersions(ProductVersionList) should create multiple new ProductVersions")]
        public async void PostMultipleProductVersionsCorrectDataCreatesProductVersions()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                IEnumerable<ProductVersionDto> ProductVersionList = Enumerable.Range(101, 10).Select(i => GetTestProductVersionDto(i));

                var result = await controller.PostMultipleProductVersions(1, ProductVersionList);

                Assert.NotNull(result);
                Assert.IsType<CreatedAtActionResult>(result);

            }
        }

        [Fact(DisplayName = "PostMultipleProductVersions(ProductVersionList) with ModelStateError should return BadRequest")]
        public async void PostMultipleProductVersionsModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                IEnumerable<ProductVersionDto> ProductVersionList = Enumerable.Range(11, 10).Select(i => GetTestProductVersionDto(i));

                var result = await controller.PostMultipleProductVersions(1, ProductVersionList);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region Test DELETE Methods

        // DELETE Methods
        [Fact(DisplayName = "DeleteProductVersion(id) should remove the ProductVersion from context")]
        public async void DeleteProductVersionIdDeletesProductVersion()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = await controller.DeleteProductVersion(1, 3);
                ProductVersionDto p3 = GetTestProductVersionDto(3);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.Equal(p3, resultValue);

            }
        }

        [Fact(DisplayName = "DeleteProductVersion(wrongId) should return NotFound")]
        public async void DeleteProductVersionWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = await controller.DeleteProductVersion(1, 999);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "DeleteProductVersion(id) with ModelStateError should return BadRequest")]
        public async void DeleteProductVersionModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.DeleteProductVersion(1, 1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #endregion
    }
}
