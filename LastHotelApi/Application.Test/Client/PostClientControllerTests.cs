using Application.Controllers;
using Domain.Dtos;
using Domain.Interfaces.Services.Client;
using Domain.Models;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Client
{
    public class PostClientControllerTests: ClientControllerTests
    {
        [Fact]
        public async Task Should_Return_Created_Result_When_Service_Is_Able_To_Create()
        {
            _mockMapper.Setup(m => m.Map<ClientModel>(ClientPostDto)).Returns(ClientModel);
            _mockMapper.Setup(m => m.Map<ClientPostResultDto>(ClientModel)).Returns(ClientPostResultDto);

            _mockService.Setup(m => m.Create(ClientModel)).ReturnsAsync(ClientModel);

            var controller = new ClientsController(_mockService.Object, _mockMapper.Object);
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(ClientPostDto);

            Assert.True(result is CreatedResult);
            var resultValue = ((CreatedResult)result).Value as ClientPostResultDto;
            Assert.Equal(resultValue, ClientPostResultDto);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_ModelState_Is_Invalid()
        {
            _mockService.Setup(m => m.Create(ClientModel)).ReturnsAsync(ClientModel);

            var controller = new ClientsController(_mockService.Object, _mockMapper.Object);
            controller.ModelState.AddModelError("Name", "Name is required");
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(ClientPostDto);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Service_Returns_Invalid_Result()
        {
            var expectedNotification = new Notification("test", "test message");
            ClientModel.AddNotification(expectedNotification);

            _mockService.Setup(m => m.Create(ClientModel)).ReturnsAsync(ClientModel);
            _mockMapper.Setup(m => m.Map<ClientModel>(ClientPostDto)).Returns(ClientModel);

            var controller = new ClientsController(_mockService.Object, _mockMapper.Object);
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(ClientPostDto);

            Assert.True(result is BadRequestObjectResult);
            Assert.Single(((BadRequestObjectResult)result).Value as IEnumerable<Notification>);
            Assert.Collection(((BadRequestObjectResult)result).Value as IEnumerable<Notification>, item =>
            {
                Assert.Equal(expectedNotification, item);
            });
        }
    }
}
