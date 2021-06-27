using Data.Context;
using Data.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Data.Test
{
    public class ClientRepositoryTests : RepositoryTests
    {
        private ClientEntity _clientEntity;

        public ClientRepositoryTests()
        {
            _clientEntity = new ClientEntity
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Name.FullName()
            };
        }

        [Fact]
        public async Task Should_Insert_Client_In_Database_And_Return_Entity_With_Id_And_Created_At()
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                ClientRepository repository = new ClientRepository(context);

                var result = await repository.InsertAsync(_clientEntity);

                Assert.False(result.Id == Guid.Empty);
                Assert.NotNull(result.CreatedAt);
                Assert.True(await context.Clients.AnyAsync(x => x.Id == result.Id));
            }
        }

        [Fact]
        public async Task Should_Update_Client_In_Database_And_Return_Entity_When_Record_Exists()
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                context.Clients.Add(_clientEntity);
                await context.SaveChangesAsync();
                ClientRepository repository = new ClientRepository(context);

                var modifiedEntity = new ClientEntity
                {
                    Id = _clientEntity.Id,
                    Name = Faker.Name.FullName(),
                    Email = _clientEntity.Email
                };

                var result = await repository.UpdateAsync(modifiedEntity);

                Assert.Equal(modifiedEntity.Name, result.Name);
                Assert.NotNull(result.UpdatedAt);
                Assert.Equal(modifiedEntity.Name, context.Clients.SingleOrDefaultAsync(x => x.Id == _clientEntity.Id).Result.Name);
            }
        }

        [Fact]
        public async Task Should_Return_Null_When_Updating_A_Client_That_Does_Not_Exist()
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                ClientRepository repository = new ClientRepository(context);

                var modifiedEntity = new ClientEntity
                {
                    Id = _clientEntity.Id,
                    Name = Faker.Name.FullName(),
                    Email = _clientEntity.Email
                };

                var result = await repository.UpdateAsync(modifiedEntity);

                Assert.Null(result);
            }
        }

        [Fact]
        public async Task Should_Check_If_Client_Exists_In_Database_And_Return_Result()
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                context.Clients.Add(_clientEntity);
                await context.SaveChangesAsync();

                ClientRepository repository = new ClientRepository(context);

                var recordExists = await repository.ExistsAsync(_clientEntity.Id);
                Assert.True(recordExists);
            }
        }

        [Fact]
        public async Task Should_Select_Client_By_Id_From_Database()
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                context.Clients.Add(_clientEntity);
                await context.SaveChangesAsync();

                ClientRepository repository = new ClientRepository(context);

                var record = await repository.SelectAsync(_clientEntity.Id);
                Assert.Equal(_clientEntity.Id, record.Id);
            }
        }

        [Fact]
        public async Task Should_Select_All_Clients_From_Database()
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                context.Clients.Add(_clientEntity);
                await context.SaveChangesAsync();

                ClientRepository repository = new ClientRepository(context);

                var allRecords = await repository.SelectAsync();
                Assert.NotNull(allRecords);
                Assert.Single(allRecords);
            }
        }

        [Fact]
        public async Task Should_Delete_Client_From_Database_And_Return_True_When_Record_Exists()
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                context.Clients.Add(_clientEntity);
                await context.SaveChangesAsync();

                ClientRepository repository = new ClientRepository(context);

                var result = await repository.DeleteAsync(_clientEntity.Id);
                
                Assert.True(result);
                Assert.Empty(await context.Clients.ToListAsync());
            }
        }

        [Fact]
        public async Task Should_Return_True_When_Deleting_A_Record_That_Does_Not_Exist()
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                await context.SaveChangesAsync();

                ClientRepository repository = new ClientRepository(context);

                var result = await repository.DeleteAsync(_clientEntity.Id);

                Assert.False(result);
            }
        }
    }
}
