using AutoMapper;
using SvarosNamai.Serivce.OrderAPI.Models;
using SvarosNamai.Serivce.OrderAPI.Models.Dtos;
using SvarosNamai.Service.OrderAPI.Data;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System.Collections.Generic;
using System.Linq;


namespace SvarosNamai.Service.OrderAPI
{
    public class MappingConfig
    {


        private static AppDbContext _db;

        public MappingConfig(AppDbContext db)
        {
            _db = db;
        }
        public static MapperConfiguration RegisterMaps()
        {




            var mappingConfig = new MapperConfiguration(config =>
            {

                config.CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Reservation, opt => opt.MapFrom(src => new Reservations()
                {
                    Date = src.Date,
                    Hour = src.Hour
                }));

                config.CreateMap<OrderLine, OrderLineDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.OrderId));

                config.CreateMap<OrderLineDto, OrderLine>();






                config.CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Reservation.Date))
                .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.Reservation.Hour));



                config.CreateMap<Order, ConfirmationEmailDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Reservation.Date))
                .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.Reservation.Hour))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Street} {src.HouseNo}{src.ApartmentNo}{src.HouseLetter}, {src.City}"));

                config.CreateMap<Order, OrderForInvoiceDto>();
                config.CreateMap<OrderLine, OrderLinesForInvoiceDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.OrderId));

                config.CreateMap<Reservations, ReservationsDto>();



            });
            return mappingConfig;
        }
    }
}
