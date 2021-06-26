using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }

    public class TestDb : IDisposable
    {
        private string dataBaseName = $"testDb_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }

        public TestDb()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<HotelContext>(o =>
            {
                o.UseInMemoryDatabase(dataBaseName);
            }, ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<HotelContext>())
            {
                context.Database.EnsureCreated();
            }
        }
        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<HotelContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
