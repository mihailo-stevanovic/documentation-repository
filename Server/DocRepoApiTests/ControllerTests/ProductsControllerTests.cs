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


        // Get Methods
        [Fact(DisplayName = "GetProducts() should return a list of all products")]
        public void GetProductsReturnsListOfAuthors()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                var result = controller.GetProducts();
                ProductDto p3 = GetTestProductDto(3);

                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result);
                Assert.Equal(10, result.Count());
                ProductDto p = (ProductDto)result.Where(r => r.Id == 3).FirstOrDefault();
                Assert.True(p.Equals(p3));
                Assert.True(p.Equals(p3, true));

            }
        }
        
        [Fact(DisplayName = "GetProduct(id) should return the Product with the the ID")]
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

        [Fact(DisplayName = "PostMultipleProducts(ProductList) should create a muultiple new Products")]
        public async void PostMultipleProductsCorrectDataCreatesProducts()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new ProductsController(context, _mapper))
            {
                IEnumerable<ProductDto> ProductList = Enumerable.Range(11, 10).Select(i => GetTestProductDto(i));

                var result = await controller.PostMultipleProducts(ProductList);

                Assert.NotNull(result);
                Assert.IsType<NoContentResult>(result);

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
        

    }
}
