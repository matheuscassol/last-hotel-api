using Application.Controllers;
using Domain.Dtos.Booking;
using Domain.Interfaces.Services.Booking;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booking
{
    public class PostBookingControllerTests: BookingControllerTests
    {
        [Fact]
        public async Task Should_Return_Created_Result_When_Service_Is_Able_To_Create()
        {
            _mockService.Setup(m => m.Create(BookingCreateDto)).ReturnsAsync(BookingCreateResultDto);

            var controller = new BookingsController(_mockService.Object);
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(BookingCreateDto);

            Assert.True(result is CreatedResult);
            var resultValue = ((CreatedResult)result).Value as BookingCreateResultDto;
            Assert.Equal(resultValue, BookingCreateResultDto);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_ModelState_Is_Invalid()
        {
            _mockService.Setup(m => m.Create(BookingCreateDto)).ReturnsAsync(BookingCreateResultDto);
            var controller = new BookingsController(_mockService.Object);
            controller.ModelState.AddModelError("Id", "Id is required");
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(BookingCreateDto);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Service_Returns_Null()
        {
            _mockService.Setup(m => m.Create(BookingCreateDto)).ReturnsAsync((BookingCreateResultDto)null);
            
            var controller = new BookingsController(_mockService.Object);
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(BookingCreateDto);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Service_Returns_Invalid_Result()
        {
            var expectedNotification = new Notification("test", "test message");
            BookingCreateResultDto.AddNotification(expectedNotification);
            
            _mockService.Setup(m => m.Create(BookingCreateDto)).ReturnsAsync(BookingCreateResultDto);
            
            var controller = new BookingsController(_mockService.Object);
            controller.Url = _mockUrl.Object;

            var result = await controller.Post(BookingCreateDto);

            Assert.True(result is BadRequestObjectResult);
            Assert.Single(((BadRequestObjectResult)result).Value as IEnumerable<Notification>);
            Assert.Collection(((BadRequestObjectResult)result).Value as IEnumerable<Notification>, item =>
            {
                Assert.Equal(expectedNotification, item);
            });
        }
    }
}
