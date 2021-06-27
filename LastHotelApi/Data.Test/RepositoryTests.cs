using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Data.Test
{
    public abstract class RepositoryTests
    {
        private string dataBaseName = $"testDb_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        protected ServiceProvider _serviceProvider { get; private set; }
        public RepositoryTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<HotelContext>(o =>
            {
                o.UseInMemoryDatabase(dataBaseName);
            }, ServiceLifetime.Transient);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
