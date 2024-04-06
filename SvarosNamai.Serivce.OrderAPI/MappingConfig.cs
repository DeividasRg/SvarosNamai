using AutoMapper;
using SvarosNamai.Serivce.OrderAPI.Models;


namespace SvarosNamai.Service.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Order, OrderDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
