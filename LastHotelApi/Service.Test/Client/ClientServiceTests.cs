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
        public List<ClientSelectResultDto> ClientSelectResultDtoList { get; set; } = new List<ClientSelectResultDto>();
        public ClientSelectResultDto ClientSelectResultDto { get; set; }
        public ClientCreateDto ClientCreateDto { get; set; }
        public ClientCreateResultDto ClientCreateResultDto { get; set; }
        public ClientUpdateDto ClientUpdateDto { get; set; }
        public ClientUpdateResultDto ClientUpdateResultDto { get; set; }
        public List<ClientEntity> ClientEntities { get; set; } = new List<ClientEntity>();
        public ClientEntity ClientEntity { get; set; }
        public ClientModel ClientModel { get; set; }

        public ClientServiceTests()
        {
            ClientId = Guid.NewGuid();

            for (int i = 0; i< 10; i++)
            {
                var dto = new ClientSelectResultDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };
                ClientSelectResultDtoList.Add(dto);

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

            ClientSelectResultDto = new ClientSelectResultDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            ClientCreateDto = new ClientCreateDto
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            ClientCreateResultDto = new ClientCreateResultDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreatedAt = DateTime.UtcNow
            };

            ClientUpdateDto = new ClientUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            ClientUpdateResultDto = new ClientUpdateResultDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

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
