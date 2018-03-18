using AutoMapper;
using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ModelTests
{
    public class ProductDtoTests
    {
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        [Fact(DisplayName = "ProductDto.Equals(other) should math based on id and all properties")]
        public void ProductDtoEqualsReturnsCorrectValues()
        {
            ProductDto p1 = new ProductDto
            {
                Id = 1,
                Alias = "ALIAS",
                FullName = "Great Product",
                ShortName = "GP"               
            };
            ProductDto p2 = new ProductDto
            {
                Id = 1,
                Alias = "ALIAS",
                FullName = "Great Product",
                ShortName = "GP"
            };
            ProductDto p3 = new ProductDto
            {
                Id = 3,
                Alias = "ALIAS",
                FullName = "Great Product",
                ShortName = "GP"
            };
            ProductDto p4 = new ProductDto
            {
                Id = 1,
                Alias = "SALIA",
                FullName = "Bad Product",
                ShortName = "BP"
            };

            Assert.True(p1.Equals(p2));
            Assert.True(p1.Equals(p2, true));
            Assert.False(p1.Equals(p3));
            Assert.False(p1.Equals(p3, true));
            Assert.True(p1.Equals(p4));
            Assert.False(p1.Equals(p4, true));
        }

        [Fact(DisplayName = "List<ProductDto>.Sort() should sort authors based on Alias")]
        public void ProductDtoSortReturnsListSortedByFullName()
        {
            List<ProductDto> products = new List<ProductDto>
            {
                new ProductDto { Id = 1, FullName = "BBB" }, // 2
                new ProductDto { Id = 2, FullName = "AAA" }, // 0
                new ProductDto { Id = 3, FullName = "DDD" }, // 4
                new ProductDto { Id = 4, FullName = "CCC"},  // 3
                new ProductDto { Id = 5, FullName = "ZZZ"},  // 7
                new ProductDto { Id = 6, FullName = "ABC"},  // 1
                new ProductDto { Id = 7, FullName = "DEF"},  // 5
                new ProductDto { Id = 8, FullName = "XYZ"}   // 6
            };

            products.Sort();

            Assert.True(products[0].Id.Equals(2));
            Assert.True(products[1].Id.Equals(6));
            Assert.True(products[2].Id.Equals(1));
            Assert.True(products[3].Id.Equals(4));
            Assert.True(products[4].Id.Equals(3));
            Assert.True(products[5].Id.Equals(7));
            Assert.True(products[6].Id.Equals(8));
            Assert.True(products[7].Id.Equals(5));

        }

        [Fact(DisplayName = "Product is properly mapped to ProductDto")]
        public void ProductProperlyMappedToProductDto()
        {
            ProductDto pDto1 = new ProductDto
            {
                Id = 1,
                Alias = "ALIAS",
                FullName = "Great Product",
                ShortName = "GP"
            };
            Product p1 = new Product
            {
                Id = 1,
                Alias = "ALIAS",
                FullName = "Great Product",
                ShortName = "GP"
            };

            ProductDto pDto2 = _mapper.Map<ProductDto>(p1);

            Assert.NotNull(pDto2);
            Assert.True(pDto1.Equals(pDto2));
            Assert.True(pDto1.Equals(pDto2, true));
        }

        [Fact(DisplayName = "ProductDto is properly reversed to Product")]
        public void ProductDtoProperlyReversedToProduct()
        {
            ProductDto pDto1 = new ProductDto
            {
                Id = 1,
                Alias = "ALIAS",
                FullName = "Great Product",
                ShortName = "GP"
            };
            Product p1 = new Product
            {
                Id = 1,
                Alias = "ALIAS",
                FullName = "Great Product",
                ShortName = "GP"
            };

            Product p2 = _mapper.Map<Product>(pDto1);

            Assert.NotNull(p2);
            Assert.True(p1.Equals(p2));
            Assert.True(p1.Equals(p2, true));
        }
    }
}
