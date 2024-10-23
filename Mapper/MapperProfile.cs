using AutoMapper;
using WebApp_vSem3.DTO;
using WebApp_vSem3.Models;

namespace WebApp_vSem3.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupModel>().ReverseMap();
            CreateMap<Storage, StorageModel>().ReverseMap();
        }
    }
}
