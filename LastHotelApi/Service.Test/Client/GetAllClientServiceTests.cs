using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Test.Client
{
    public class GetAllClientServiceTests: ClientServiceTests
    {
        [Fact]
        public async Task Should_Get_All_Clients()
        {
            _mockRepository.Setup(m => m.SelectAsync()).ReturnsAsync(ClientEntities);
            _mockMapper.Setup(m => m.Map<IEnumerable<ClientSelectResultDto>>(ClientEntities)).Returns(ClientSelectResultDtoList);
            var service = new ClientService(_mockRepository.Object, _mockMapper.Object);
            
            var result = await service.GetAll();

            Assert.Equal(result, ClientSelectResultDtoList);
        }
    }
}
