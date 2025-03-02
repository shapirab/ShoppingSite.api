using AutoMapper;
using ShoppingSite.api.Data.DataModels.DTO;
using ShoppingSite.api.Data.DataModels.Entity;
using ShoppingSite.api.Data.DataModels.Model;
using ShoppingSite.api.Data.Services.Interfaces;
using ShoppingSite.api.Data.Services.SqlImplementations;

namespace ShoppingSite.api.Profiles
{
    public class ShoppingProfile : Profile
    {
        public ShoppingProfile()
        {
            CreateMap<UserEntity, User>();
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, UserDTO>();
            CreateMap<UserDTO, UserEntity>();

            CreateMap<ProductEntity, Product>();
            CreateMap<Product, ProductEntity>();
            CreateMap<ProductEntity, ProductDto>();
            CreateMap<ProductDto, ProductEntity>();
        }
    }
}
