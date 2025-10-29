using AutoMapper;
using TestTask.Application.DTOs;
using TestTask.Domain.Entities;

namespace TestTask.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PriceRub, opt => opt.MapFrom(src => src.PriceRub))
                .ForMember(dest => dest.CommonNote, opt => opt.MapFrom(src => src.CommonNote))
                .ForMember(dest => dest.SpecialNote, opt => opt.MapFrom(src => src.SpecialNote))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PriceRub, opt => opt.MapFrom(src => src.PriceRub))
                .ForMember(dest => dest.CommonNote, opt => opt.MapFrom(src => src.CommonNote))
                .ForMember(dest => dest.SpecialNote, opt => opt.MapFrom(src => src.SpecialNote))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<Product, ProductUpsertDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PriceRub, opt => opt.MapFrom(src => src.PriceRub))
                .ForMember(dest => dest.CommonNote, opt => opt.MapFrom(src => src.CommonNote))
                .ForMember(dest => dest.SpecialNote, opt => opt.MapFrom(src => src.SpecialNote))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PriceRub, opt => opt.MapFrom(src => src.PriceRub))
                .ForMember(dest => dest.CommonNote, opt => opt.MapFrom(src => src.CommonNote))
                .ForMember(dest => dest.SpecialNote, opt => opt.MapFrom(src => src.SpecialNote))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category { Id = src.CategoryId }));
        }
    }
}
