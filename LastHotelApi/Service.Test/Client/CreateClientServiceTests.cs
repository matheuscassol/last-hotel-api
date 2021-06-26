using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Moq;
using Service.Services;
using Service.Test.Booking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Test.Client
{
    public class CreateClientServiceTests: ClientServiceTests
    {
        [Fact]
        public async Task Should_Insert_Client_And_Return_Result()
        {
            _mockRepository.Setup(m => m.InsertAsync(ClientEntity)).ReturnsAsync(ClientEntity);
            
            _mockMapper.Setup(m => m.Map<ClientModel>(ClientCreateDto)).Returns(ClientModel);
            _mockMapper.Setup(m => m.Map<ClientEntity>(ClientModel)).Returns(ClientEntity);
            _mockMapper.Setup(m => m.Map<ClientCreateResultDto>(ClientEntity)).Returns(ClientCreateResultDto);
            
            var service = new ClientService(_mockRepository.Object, _mockMapper.Object);
            var result = await service.Create(ClientCreateDto);

            Assert.Equal(result, ClientCreateResultDto);
        }
    }
}
