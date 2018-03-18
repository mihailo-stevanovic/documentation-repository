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
            CreateMap<ClientCatalog, ClientCatalogDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductVersion, ProductVersionDto>().ForMember(d => d.Product, opt => opt.MapFrom(src => src.Product.FullName))
                .ReverseMap().ForPath(s => s.Product, opt => opt.Ignore());
            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
        }
    }
}
