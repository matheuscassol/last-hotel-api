using AutoMapper;
using Domain.Dtos;
using Domain.Interfaces.Repositories;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Test.Client
{
    public class DeleteClientServiceTests: ClientServiceTests
    {
        [Fact]
        public async Task Should_Delete_Client()
        {
            _mockRepository.Setup(m => m.DeleteAsync(ClientId)).ReturnsAsync(true);
            var service = new ClientService(_mockRepository.Object, _mockMapper.Object);
            
            var result = await service.Delete(ClientId);

            Assert.True(result);
        }
    }
}
