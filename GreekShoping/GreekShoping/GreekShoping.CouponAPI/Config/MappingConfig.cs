using AutoMapper;
using GreekShoping.CouponAPI.Data.ValueObjects;
using GreekShoping.CouponAPI.Models;

namespace GreekShoping.CouponAPI.Config;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps() 
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Coupon, CouponVO>().ReverseMap();
        });
        return mapperConfig;
    }
}
