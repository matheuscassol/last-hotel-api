using Data.Context;
using Data.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Data.Test
{
    public class ClientCrudTests : BaseTest, IClassFixture<TestDb>
    {
        private ServiceProvider _serviceProvider;

        public ClientCrudTests(TestDb testDb)
        {
            _serviceProvider = testDb.ServiceProvider;
        }

        [Fact(DisplayName = "Client CRUD")]
        public async Task Should_Execute_Client_Crud_Operations()
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                ClientRepository repository = new ClientRepository(context);
                ClientEntity entity = new ClientEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var createdRecord = await repository.InsertAsync(entity);
                Assert.NotNull(createdRecord);
                Assert.Equal(createdRecord.Email, entity.Email);
                Assert.Equal(createdRecord.Name, entity.Name);
                Assert.False(createdRecord.Id == Guid.Empty);
                Assert.NotNull(createdRecord.CreatedAt);

                entity.Name = Faker.Name.First();
                var updatedRecord = await repository.UpdateAsync(entity);
                Assert.Equal(updatedRecord.Email, createdRecord.Email);
                Assert.Equal(updatedRecord.Name, createdRecord.Name);
                Assert.Equal(updatedRecord.Id, createdRecord.Id);
                Assert.NotNull(updatedRecord.UpdatedAt);

                var recordExists = await repository.ExistsAsync(updatedRecord.Id);
                Assert.True(recordExists);

                var selectedRecord = await repository.SelectAsync(updatedRecord.Id);
                Assert.NotNull(selectedRecord);
                Assert.Equal(updatedRecord.Email, selectedRecord.Email);
                Assert.Equal(updatedRecord.Name, selectedRecord.Name);
                Assert.Equal(updatedRecord.Id, selectedRecord.Id);

                var allRecords = await repository.SelectAsync();
                Assert.NotNull(allRecords);
                Assert.True(allRecords.Count() > 0);

                var isRecordRemoved = await repository.DeleteAsync(selectedRecord.Id);
                Assert.True(isRecordRemoved);
            }
        }
    }
}
