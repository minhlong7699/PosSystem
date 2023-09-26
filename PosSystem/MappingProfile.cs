using AutoMapper;
using Entity.Models;
using Shared.DataTransferObjects;

namespace PosSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();
        }
    }
}
