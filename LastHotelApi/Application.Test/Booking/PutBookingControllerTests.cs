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
    public class PutBookingControllerTests: BookingControllerTests
    {
        [Fact]
        public async Task Should_Return_Ok_When_Service_Is_Able_To_Update()
        {
            _mockService.Setup(m => m.Edit(BookingUpdateDto)).ReturnsAsync(BookingUpdateResultDto);
            var controller = new BookingsController(_mockService.Object);

            var result = await controller.Put(BookingUpdateDto);

            Assert.True(result is OkObjectResult);
            Assert.Equal(BookingUpdateResultDto, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_ModelState_Is_Invalid()
        {
            _mockService.Setup(m => m.Edit(BookingUpdateDto)).ReturnsAsync(BookingUpdateResultDto);

            var controller = new BookingsController(_mockService.Object);
            controller.ModelState.AddModelError("Name", "Name is required");

            var result = await controller.Put(BookingUpdateDto);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Service_Returns_Null()
        {
            _mockService.Setup(m => m.Edit(BookingUpdateDto)).ReturnsAsync((BookingUpdateResultDto)null);
            var controller = new BookingsController(_mockService.Object);
            
            var result = await controller.Put(BookingUpdateDto);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Service_Returns_Invalid_Result()
        {
            var expectedNotification = new Notification("test", "test message");
            BookingUpdateResultDto.AddNotification(expectedNotification);
            _mockService.Setup(m => m.Edit(BookingUpdateDto)).ReturnsAsync(BookingUpdateResultDto);

            var controller = new BookingsController(_mockService.Object);
            controller.Url = _mockUrl.Object;

            var result = await controller.Put(BookingUpdateDto);

            Assert.True(result is BadRequestObjectResult);
            Assert.Single(((BadRequestObjectResult)result).Value as IEnumerable<Notification>);
            Assert.Collection(((BadRequestObjectResult)result).Value as IEnumerable<Notification>, item =>
            {
                Assert.Equal(expectedNotification, item);
            });
        }
    }
}
