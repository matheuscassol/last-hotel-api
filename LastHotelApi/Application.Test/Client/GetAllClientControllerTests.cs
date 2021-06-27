using Application.Controllers;
using Domain.Dtos;
using Domain.Interfaces.Services.Client;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Client
{
    public class GetAllClientControllerTests: ClientControllerTests
    {
        [Fact]
        public async Task Should_Return_Ok_When_Service_Is_Able_To_Get_All_Clients()
        {
            _mockMapper.Setup(m => m.Map<IEnumerable<ClientGetResultDto>>(ClientModels)).Returns(ClientGetResultDtos);
            _mockService.Setup(m => m.GetAll()).ReturnsAsync(ClientModels);
            var controller = new ClientsController(_mockService.Object, _mockMapper.Object);

            var result = await controller.GetAll();

            Assert.True(result is OkObjectResult);
            Assert.Equal(ClientGetResultDtos, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_ModelState_Is_Invalid()
        {
            _mockService.Setup(m => m.GetAll()).ReturnsAsync(ClientModels);

            var controller = new ClientsController(_mockService.Object, _mockMapper.Object);
            controller.ModelState.AddModelError("Id", "Id is required");

            var result = await controller.GetAll();

            Assert.True(result is BadRequestObjectResult);
        }
    }
}
