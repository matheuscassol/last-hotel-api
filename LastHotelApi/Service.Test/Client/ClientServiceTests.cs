using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Service.Test.Client
{
    public abstract class ClientServiceTests
    {
        protected Mock<IClientRepository> _mockRepository = new Mock<IClientRepository>();
        protected Mock<IMapper> _mockMapper = new Mock<IMapper>();

        public Guid ClientId { get; set; }
        public ClientModel ClientModel { get; set; }
        public List<ClientModel> ClientModels { get; set; } = new List<ClientModel>();
        public ClientEntity ClientEntity { get; set; }
        public List<ClientEntity> ClientEntities { get; set; } = new List<ClientEntity>();

        public ClientServiceTests()
        {
            ClientId = Guid.NewGuid();

            for (int i = 0; i< 10; i++)
            {
                var model = new ClientModel
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };
                ClientModels.Add(model);

                var entity = new ClientEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                ClientEntities.Add(entity);
            }

            ClientEntity = new ClientEntity
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            ClientModel = new ClientModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

    }
}
