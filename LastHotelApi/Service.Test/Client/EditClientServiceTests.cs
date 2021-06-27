using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
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
    public class EditClientServiceTests: ClientServiceTests
    {
        [Fact]
        public async Task Should_Edit_Client_And_Return_Result()
        {
            _mockRepository.Setup(m => m.UpdateAsync(ClientEntity)).ReturnsAsync(ClientEntity);
            
            _mockMapper.Setup(m => m.Map<ClientEntity>(ClientModel)).Returns(ClientEntity);
            _mockMapper.Setup(m => m.Map<ClientModel>(ClientEntity)).Returns(ClientModel);
            var service = new ClientService(_mockRepository.Object, _mockMapper.Object);

            var result = await service.Edit(ClientModel);

            Assert.Equal(result, ClientModel);
        }
    }
}
