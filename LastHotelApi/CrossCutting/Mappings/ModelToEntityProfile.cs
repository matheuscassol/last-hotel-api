using AutoMapper;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Mappings
{
    public class ModelToEntityProfile: Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<ClientModel, ClientEntity>()
                .ReverseMap();

            CreateMap<BookingModel, BookingEntity>()
                .ReverseMap();
        }
    }
}
