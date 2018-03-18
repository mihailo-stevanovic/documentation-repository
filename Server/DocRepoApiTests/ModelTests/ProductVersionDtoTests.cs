using AutoMapper;
using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ModelTests
{
    public class ProductVersionDtoTests
    {
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        #region Test Compare and Sort
        [Fact(DisplayName = "ProductVersionDto.Equals(other) should math based on id and all properties")]
        public void ProductVersionDtoEqualsReturnsCorrectValues()
        {
            ProductVersionDto p1 = new ProductVersionDto
            {
                Id = 1,
                Product = "Great Product",
                Release = "V1",
                EndOfSupport = DateTime.Today
            };
            ProductVersionDto p2 = new ProductVersionDto
            {
                Id = 1,
                Product = "Great Product",
                Release = "V1",
                EndOfSupport = DateTime.Today
            };
            ProductVersionDto p3 = new ProductVersionDto
            {
                Id = 3,
                Product = "Great Product",
                Release = "V1",
                EndOfSupport = DateTime.Today
            };
            ProductVersionDto p4 = new ProductVersionDto
            {
                Id = 1,
                Product = "Great Product",
                Release = "V2",
                EndOfSupport = DateTime.Today
            };

            Assert.True(p1.Equals(p2));
            Assert.True(p1.Equals(p2, true));
            Assert.False(p1.Equals(p3));
            Assert.False(p1.Equals(p3, true));
            Assert.True(p1.Equals(p4));
            Assert.False(p1.Equals(p4, true));
        }

        [Fact(DisplayName = "List<ProductVersionDto>.Sort() should sort authors based on ProductFullName then Release")]
        public void ProductVersionDtoSortReturnsListSortedByProductThenVersion()
        {
            List<ProductVersionDto> ProductVersions = new List<ProductVersionDto>
            {
                new ProductVersionDto { Id = 1, Product = "BBB", Release = "V2018.3" }, // 2
                new ProductVersionDto { Id = 2, Product = "AAA", Release = "V2018.2" }, // 0
                new ProductVersionDto { Id = 3, Product = "BBB", Release = "V2018.1" }, // 4
                new ProductVersionDto { Id = 4, Product = "BBB", Release = "V2018.2" },  // 3
                new ProductVersionDto { Id = 5, Product = "CCC", Release = "V2017.7" },  // 7
                new ProductVersionDto { Id = 6, Product = "AAA", Release = "V2018.1" },  // 1
                new ProductVersionDto { Id = 7, Product = "CCC", Release = "V2018.2" },  // 5
                new ProductVersionDto { Id = 8, Product = "CCC", Release = "V2018.1" }   // 6
            };

            ProductVersions.Sort();

            Assert.True(ProductVersions[0].Id.Equals(2));
            Assert.True(ProductVersions[1].Id.Equals(6));
            Assert.True(ProductVersions[2].Id.Equals(1));
            Assert.True(ProductVersions[3].Id.Equals(4));
            Assert.True(ProductVersions[4].Id.Equals(3));
            Assert.True(ProductVersions[5].Id.Equals(7));
            Assert.True(ProductVersions[6].Id.Equals(8));
            Assert.True(ProductVersions[7].Id.Equals(5));

        }
        #endregion
        
        #region Test Mapping
        [Fact(DisplayName = "ProductVersion is properly mapped to ProductVersionDto")]
        public void ProductVersionProperlyMappedToProductVersionDto()
        {
            ProductVersionDto pDto1 = new ProductVersionDto
            {
                Id = 1,
                Product = "Great Product",
                Release = "V1",
                EndOfSupport = DateTime.Today
            };

            ProductVersion p1 = new ProductVersion
            {
                Id = 1,
                Product = new Product
                {
                    FullName = "Great Product"
                },
                Release = "V1",
                EndOfSupport = DateTime.Today
            };

            ProductVersionDto pDto2 = _mapper.Map<ProductVersionDto>(p1);

            Assert.NotNull(pDto2);
            Assert.True(pDto1.Equals(pDto2));
            Assert.True(pDto1.Equals(pDto2, true));
        }

        [Fact(DisplayName = "ProductVersionDto is properly reversed to ProductVersion")]
        public void ProductVersionDtoProperlyReversedToProductVersion()
        {
            ProductVersionDto pDto1 = new ProductVersionDto
            {
                Id = 1,
                Product = "Great Product",
                Release = "V1",
                EndOfSupport = DateTime.Today
            };

            ProductVersion p1 = new ProductVersion
            {
                Id = 1,
                Product = new Product
                {
                    FullName = "Great Product"
                },
                Release = "V1",
                EndOfSupport = DateTime.Today
            };

            ProductVersion p2 = _mapper.Map<ProductVersion>(pDto1);

            Assert.NotNull(p2);
            Assert.True(p1.Equals(p2));
            Assert.True(p1.Equals(p2, true));
        }
        #endregion
    }
}
