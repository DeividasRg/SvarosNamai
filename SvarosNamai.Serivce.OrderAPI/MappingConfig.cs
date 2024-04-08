using AutoMapper;
using SvarosNamai.Serivce.OrderAPI.Models;
using SvarosNamai.Service.OrderAPI.Models.Dtos;


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
                config.CreateMap<Order, ConfirmationEmailDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Reservation.Date))
                .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.Reservation.Hour))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Street} {src.HouseNo}{src.ApartmentNo}{src.HouseLetter}, {src.City}"));
                
                
            });
            return mappingConfig;
        }
    }
}
