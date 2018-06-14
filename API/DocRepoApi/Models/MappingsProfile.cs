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
            CreateMap<DocumentUpdate, DocumentUpdateDto>().ReverseMap();
            CreateMap<Document, DocumentDtoInternal>()
                .ForMember(d => d.Authors, opt => opt.MapFrom(src => src.DocumentAuthors.Select(da => da.Author)))
                .ForMember(d => d.ClientCatalogs, opt => opt.MapFrom(src => src.DocumentCatalogs.Select(dc => dc.Catalog)))
                .ForMember(d => d.DocumentType, opt => opt.MapFrom(src => src.DocumentType.FullName))
                .ForMember(d => d.Product, opt => opt.MapFrom(src => src.ProductVersion.Product.FullName))
                .ForMember(d => d.Version, opt => opt.MapFrom(src => src.ProductVersion.Release))
                .ForMember(d => d.LatestUpdate, opt => opt.MapFrom(src => src.Updates.OrderByDescending(u => u.Timestamp).FirstOrDefault(u => u.IsPublished).Timestamp))
                .ForMember(d => d.LatestTopicsUpdated, opt => opt.MapFrom(src => src.Updates.OrderByDescending(u => u.Timestamp).FirstOrDefault(u => u.IsPublished).LatestTopicsUpdated));
            CreateMap<Document, DocumentDto>()
                .ForMember(d => d.DocumentAuthorIds, opt => opt.MapFrom(src => src.DocumentAuthors.Select(da => da.AuthorId)))
                .ForMember(d => d.DocumentCatalogIds, opt => opt.MapFrom(src => src.DocumentCatalogs.Select(dc => dc.CatalogId)))
                .ForMember(d => d.IsPublished, opt => opt.MapFrom(src => src.Updates.OrderByDescending(u => u.Timestamp).FirstOrDefault().IsPublished))
                .ForMember(d => d.LatestTopicsUpdated, opt => opt.MapFrom(src => src.Updates.OrderByDescending(u => u.Timestamp).FirstOrDefault().LatestTopicsUpdated))
                .ReverseMap()
                .ForPath(s => s.DocumentAuthors, opt => opt.Ignore())
                .ForPath(s => s.DocumentCatalogs, opt => opt.Ignore());
        }
    }
}
