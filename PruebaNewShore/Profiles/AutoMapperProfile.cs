using AutoMapper;
using PruebaNewShore.DTOs;
using PruebaNewShore.Models;

namespace PruebaNewShore.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FlightDTO, Flight>()
                .ForPath(m => m.Transport.FlightCarrier, opt => opt.MapFrom(src => src.FlightCarrier))
                .ForPath(m => m.Transport.FlightNumber, opt => opt.MapFrom(src => src.FlightNumber))
                .ForMember(m => m.Origin, opt => opt.MapFrom(src => src.DepartureStation))
                .ForMember(m => m.Destination, opt => opt.MapFrom(src => src.ArrivalStation));
        }
    }
}
