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
    public class PutClientControllerTests: ClientControllerTests
    {
        [Fact]
        public async Task Should_Return_Ok_When_Service_Is_Able_To_Update()
        {
            _mockService.Setup(m => m.Edit(ClientUpdateDto)).ReturnsAsync(ClientUpdateResultDto);
            var controller = new ClientsController(_mockService.Object);

            var result = await controller.Put(ClientUpdateDto);

            Assert.True(result is OkObjectResult);
            Assert.Equal(ClientUpdateResultDto, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_ModelState_Is_Invalid()
        {
            _mockService.Setup(m => m.Edit(ClientUpdateDto)).ReturnsAsync(ClientUpdateResultDto);

            var controller = new ClientsController(_mockService.Object);
            controller.ModelState.AddModelError("Name", "Name is required");

            var result = await controller.Put(ClientUpdateDto);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Service_Returns_Null()
        {
            _mockService.Setup(m => m.Edit(ClientUpdateDto)).ReturnsAsync((ClientUpdateResultDto)null);
            var controller = new ClientsController(_mockService.Object);
            
            var result = await controller.Put(ClientUpdateDto);

            Assert.True(result is BadRequestResult);
        }
    }
}
