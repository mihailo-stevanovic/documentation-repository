using AutoMapper;
using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ModelTests
{
    public class ClientCatalogDtoTests
    {
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        #region Test Compare and Sort
        [Fact(DisplayName = "ClientCatalogDto.Equals(other) should match based on id and all properties")]
        public void ClientCatalogDtoEqualsRetursCorrectValues()
        {
            ClientCatalogDto c1 = new ClientCatalogDto
            {
                Id = 1,
                Name = "Client Cat 1",
                InternalId = "CT1"                
            };
            ClientCatalogDto c2 = new ClientCatalogDto
            {
                Id = 1,
                Name = "Client Cat 1",
                InternalId = "CT1"
            };
            ClientCatalogDto c3 = new ClientCatalogDto
            {
                Id = 3,
                Name = "Client Cat 1",
                InternalId = "CT1"
            };
            ClientCatalogDto c4 = new ClientCatalogDto
            {
                Id = 1,
                Name = "Client Cat 4",
                InternalId = "CT4"
            };

            Assert.True(c1.Equals(c2));
            Assert.True(c1.Equals(c2, true));
            Assert.False(c1.Equals(c3));
            Assert.False(c1.Equals(c3, true));
            Assert.True(c1.Equals(c4));
            Assert.False(c1.Equals(c4, true));
        }

        [Fact(DisplayName = "List<ClientCatalogDto>.Sort() should sort authors based on Category then FullName")]
        public void ClientCatalogDtoSortReturnsListSortedByFullName()
        {
            List<ClientCatalogDto> ClientCatalogs = new List<ClientCatalogDto>
            {
                new ClientCatalogDto { Id = 1, Name = "BBB" }, // 2
                new ClientCatalogDto { Id = 2, Name = "AAA" }, // 0
                new ClientCatalogDto { Id = 3, Name = "DDD" }, // 4
                new ClientCatalogDto { Id = 4, Name = "CCC"},  // 3
                new ClientCatalogDto { Id = 5, Name = "ZZZ"},  // 7
                new ClientCatalogDto { Id = 6, Name = "ABC"},  // 1
                new ClientCatalogDto { Id = 7, Name = "DEF"},  // 5
                new ClientCatalogDto { Id = 8, Name = "XYZ"}   // 6
            };

            ClientCatalogs.Sort();

            Assert.True(ClientCatalogs[0].Id.Equals(2));
            Assert.True(ClientCatalogs[1].Id.Equals(6));
            Assert.True(ClientCatalogs[2].Id.Equals(1));
            Assert.True(ClientCatalogs[3].Id.Equals(4));
            Assert.True(ClientCatalogs[4].Id.Equals(3));
            Assert.True(ClientCatalogs[5].Id.Equals(7));
            Assert.True(ClientCatalogs[6].Id.Equals(8));
            Assert.True(ClientCatalogs[7].Id.Equals(5));

        }
        #endregion

        #region Test Mapping
        [Fact(DisplayName = "ClientCatalog is properly mapped to ClientCatalogDto")]
        public void ClientCatalogProperlyMappedToClientCatalogDto()
        {
            ClientCatalogDto cDto1 = new ClientCatalogDto
            {
                Id = 1,
                Name = "Client Cat 1",
                InternalId = "CT1"
            };
            ClientCatalog c1 = new ClientCatalog
            {
                Id = 1,
                Name = "Client Cat 1",
                InternalId = "CT1"
            };            

            ClientCatalogDto cDto2 = _mapper.Map<ClientCatalogDto>(c1);

            Assert.NotNull(cDto2);
            Assert.True(cDto1.Equals(cDto2));
            Assert.True(cDto1.Equals(cDto2, true));
        }

        [Fact(DisplayName = "ClientCatalogDto is properly reversed to ClientCatalog")]
        public void ClientCatalogDtoProperlyReversedToClientCatalog()
        {
            ClientCatalogDto cDto1 = new ClientCatalogDto
            {
                Id = 1,
                Name = "Client Cat 1",
                InternalId = "CT1"
            };
            ClientCatalog c1 = new ClientCatalog
            {
                Id = 1,
                Name = "Client Cat 1",
                InternalId = "CT1"
            };

            ClientCatalog c2 = _mapper.Map<ClientCatalog>(cDto1);

            Assert.NotNull(c2);
            Assert.True(c1.Equals(c2));
            Assert.True(c1.Equals(c2, true));
        }
        #endregion
    }
}
