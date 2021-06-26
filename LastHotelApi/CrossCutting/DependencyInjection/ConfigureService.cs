using Data.Repository;
using Domain.Interfaces;
using Domain.Interfaces.Services.Booking;
using Domain.Interfaces.Services.Client;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureServicesDependencyInjection(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IClientService, ClientService>();
            serviceColletion.AddScoped<IBookingService, BookingService>();
        }
    }
}
