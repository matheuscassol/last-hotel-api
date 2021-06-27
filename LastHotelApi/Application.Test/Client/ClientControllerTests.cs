using AutoMapper;
using Domain.Dtos;
using Domain.Interfaces.Services.Client;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
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
        protected Mock<IMapper> _mockMapper = new Mock<IMapper>();
        
        public Guid ClientId { get; set; }
        public ClientModel ClientModel { get; set; }
        public List<ClientModel> ClientModels { get; set; } = new List<ClientModel>();
        public ClientGetResultDto ClientGetResultDto { get; set; }
        public List<ClientGetResultDto> ClientGetResultDtos { get; set; } = new List<ClientGetResultDto>();
        public ClientPostDto ClientPostDto { get; set; }
        public ClientPostResultDto ClientPostResultDto { get; set; }
        public ClientPutDto ClientPutDto { get; set; }
        public ClientPutResultDto ClientPutResultDto { get; set; }

        public ClientControllerTests()
        {
            _mockUrl.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            for (int i = 0; i < 10; i++)
            {
                var model = new ClientModel
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };
                ClientModels.Add(model);

                var dto = new ClientGetResultDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };
                ClientGetResultDtos.Add(dto);
            }

            ClientGetResultDto = new ClientGetResultDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            ClientPostDto = new ClientPostDto
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            ClientPostResultDto = new ClientPostResultDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreatedAt = DateTime.UtcNow
            };

            ClientPutDto = new ClientPutDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            ClientPutResultDto = new ClientPutResultDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            ClientModel = new ClientModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };
        }
    }
}
