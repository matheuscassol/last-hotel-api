using Application.Controllers;
using Domain.Dtos;
using Domain.Interfaces.Services.Client;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
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
            _mockService.Setup(m => m.Create(ClientCreateDto)).ReturnsAsync(ClientCreateResultDto);

            var controller = new ClientsController(_mockService.Object);
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(ClientCreateDto);

            Assert.True(result is CreatedResult);
            var resultValue = ((CreatedResult)result).Value as ClientCreateResultDto;
            Assert.Equal(resultValue, ClientCreateResultDto);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_ModelState_Is_Invalid()
        {
            _mockService.Setup(m => m.Create(ClientCreateDto)).ReturnsAsync(ClientCreateResultDto);

            var controller = new ClientsController(_mockService.Object);
            controller.ModelState.AddModelError("Name", "Name is required");
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(ClientCreateDto);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Service_Returns_Null()
        {
            _mockService.Setup(m => m.Create(ClientCreateDto)).ReturnsAsync((ClientCreateResultDto)null);
            
            var controller = new ClientsController(_mockService.Object);
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(ClientCreateDto);

            Assert.True(result is BadRequestResult);
        }

        
    }
}
