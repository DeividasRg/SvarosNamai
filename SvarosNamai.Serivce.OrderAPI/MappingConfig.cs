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
                config.CreateMap<Order, OrderDto>().ReverseMap()
                                      .ForMember(dest => dest.Reservation, opt => opt.MapFrom(src => new Reservations
                                      {
                                          Date = src.Date,
                                          Hour = src.Hour
                                      }));
            });
            return mappingConfig;
        }
    }
}
