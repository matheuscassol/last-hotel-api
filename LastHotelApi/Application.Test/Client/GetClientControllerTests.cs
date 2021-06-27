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
    public class GetClientControllerTests: ClientControllerTests
    {
        [Fact]
        public async Task Should_Return_Ok_When_Service_Is_Able_To_Get_Client_By_Id()
        {
            _mockMapper.Setup(m => m.Map<ClientGetResultDto>(ClientModel)).Returns(ClientGetResultDto);

            _mockService.Setup(m => m.GetById(ClientId)).ReturnsAsync(ClientModel);
            var controller = new ClientsController(_mockService.Object, _mockMapper.Object);

            var result = await controller.Get(ClientId);

            Assert.True(result is OkObjectResult);
            Assert.Equal(ClientGetResultDto, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_ModelState_Is_Invalid()
        {
            _mockService.Setup(m => m.GetById(ClientId)).ReturnsAsync(ClientModel);

            var controller = new ClientsController(_mockService.Object, _mockMapper.Object);
            controller.ModelState.AddModelError("Id", "Id is required");

            var result = await controller.Get(ClientId);

            Assert.True(result is BadRequestObjectResult);
        }
    }
}
