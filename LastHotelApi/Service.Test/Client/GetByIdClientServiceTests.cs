using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Client;
using Domain.Models;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Test.Client
{
    public class GetByIdClientServiceTests: ClientServiceTests
    {
        [Fact]
        public async Task Should_Get_Client_By_Id()
        {
            _mockRepository.Setup(m => m.SelectAsync(ClientId)).ReturnsAsync(ClientEntity);
            _mockMapper.Setup(m => m.Map<ClientModel>(ClientEntity)).Returns(ClientModel);
            var service = new ClientService(_mockRepository.Object, _mockMapper.Object);
            
            var result = await service.GetById(ClientId);

            Assert.Equal(result, ClientModel);
        }
    }
}
