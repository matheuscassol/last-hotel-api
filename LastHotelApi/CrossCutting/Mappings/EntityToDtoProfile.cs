using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Booking;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Mappings
{
    public class EntityToDtoProfile: Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<ClientEntity, ClientGetResultDto>();
            CreateMap<ClientEntity, ClientPostResultDto>();
            CreateMap<ClientEntity, ClientPutResultDto>();
            CreateMap<BookingEntity, BookingGetResultDto>();
            CreateMap<BookingEntity, BookingPostResultDto>();
            CreateMap<BookingEntity, BookingPutResultDto>();
        }
    }
}
