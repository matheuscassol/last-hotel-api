using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Booking;
using Domain.Models;

namespace CrossCutting.Mappings
{
    public class DtoToModelProfile: Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<ClientPostDto, ClientModel>();
            CreateMap<ClientPutDto, ClientModel>();
            CreateMap<ClientModel, ClientPostResultDto>();
            CreateMap<ClientModel, ClientPutResultDto>();
            CreateMap<ClientModel, ClientGetResultDto>();

            CreateMap<BookingPostDto, BookingModel>();
            CreateMap<BookingPutDto, BookingModel>();
            CreateMap<BookingIsAvailableDto, BookingModel>();
            
            CreateMap<BookingModel, BookingGetResultDto>();
            CreateMap<BookingModel, BookingPostResultDto>();
            CreateMap<BookingModel, BookingPutResultDto>();
        }
    }
}
