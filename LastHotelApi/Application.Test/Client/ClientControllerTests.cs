using Domain.Dtos;
using Domain.Interfaces.Services.Client;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Test.Client
{
    public abstract class ClientControllerTests
    {
        protected Mock<IClientService> _mockService = new Mock<IClientService>();
        protected Mock<IUrlHelper> _mockUrl = new Mock<IUrlHelper>();
        
        public Guid ClientId { get; set; }
        public List<ClientSelectResultDto> ClientSelectResultDtoList { get; set; } = new List<ClientSelectResultDto>();
        public ClientSelectResultDto ClientSelectResultDto { get; set; }
        public ClientCreateDto ClientCreateDto { get; set; }
        public ClientCreateResultDto ClientCreateResultDto { get; set; }
        public ClientUpdateDto ClientUpdateDto { get; set; }
        public ClientUpdateResultDto ClientUpdateResultDto { get; set; }

        public ClientControllerTests()
        {
            _mockUrl.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            for (int i = 0; i < 10; i++)
            {
                var dto = new ClientSelectResultDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };
                ClientSelectResultDtoList.Add(dto);
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
        }
    }
}
