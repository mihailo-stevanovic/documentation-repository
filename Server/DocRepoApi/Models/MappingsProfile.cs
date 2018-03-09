using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
