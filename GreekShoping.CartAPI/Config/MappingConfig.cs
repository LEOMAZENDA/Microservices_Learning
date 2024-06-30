using AutoMapper;
using GreekShoping.OrderAPI.Data.ValueObjects;
using GreekShoping.OrderAPI.Models;

namespace GreekShoping.OrderAPI.Config;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps() 
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Product, ProductVO>().ReverseMap();
            config.CreateMap<Cart, CartVO>().ReverseMap();
            config.CreateMap<CartDetail, CartDetailVO>().ReverseMap();
            config.CreateMap<CartHeader, CartHeaderVO>().ReverseMap();
        });
        return mapperConfig;
    }
}
