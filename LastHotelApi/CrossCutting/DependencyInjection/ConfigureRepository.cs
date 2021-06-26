using Data.Context;
using Data.Repositories;
using Data.Repository;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureRepositoryDependencyInjection(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IClientRepository, ClientRepository>();
            serviceColletion.AddScoped<IBookingRepository, BookingRepository>();

            serviceColletion.AddDbContext<HotelContext>(
                options => options.UseInMemoryDatabase("InMemoryDb"), ServiceLifetime.Singleton
                );
        }
    }
}
