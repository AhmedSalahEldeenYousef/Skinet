using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(desc => desc.ProductBrand, options => options.MapFrom(src => src.ProductBrand.Name))
            .ForMember(desc => desc.ProductType, options => options.MapFrom(src => src.ProductType.ProductName))
            .ForMember(desc => desc.PhotoUrl, options => options.MapFrom<ProductUrlResolver>());
        }
    }
}