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
            CreateMap<ClientEntity, ClientSelectResultDto>();
            CreateMap<ClientEntity, ClientCreateResultDto>();
            CreateMap<ClientEntity, ClientUpdateResultDto>();
            CreateMap<BookingEntity, BookingSelectResultDto>();
            CreateMap<BookingEntity, BookingCreateResultDto>();
            CreateMap<BookingEntity, BookingUpdateResultDto>();
        }
    }
}
