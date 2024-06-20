using AutoMapper;
using GreekShoping.ProductApi.Data.ValueObjects;
using GreekShoping.ProductApi.Models;

namespace GreekShoping.ProductApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() 
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, Product>().ReverseMap();
            });
            return mapperConfig;
        }
    }
}
