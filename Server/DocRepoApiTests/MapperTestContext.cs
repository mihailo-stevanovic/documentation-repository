using AutoMapper;
using DocRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocRepoApiTests
{
    public static class MapperTestContext
    {
        public static IMapper GenerateTestMapperContext()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingsProfile());
            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
