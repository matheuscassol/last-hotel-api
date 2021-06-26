using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Booking;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Mappings
{
    public class DtoToModelProfile: Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<ClientCreateDto, ClientModel>();
            CreateMap<ClientUpdateDto, ClientModel>();

            CreateMap<BookingCreateDto, BookingModel>();
            CreateMap<BookingUpdateDto, BookingModel>();
            CreateMap<BookingInputDto, BookingModel>();
            CreateMap<BookingModel, BookingCreateResultDto>();
            CreateMap<BookingModel, BookingUpdateResultDto>();
        }
    }
}
