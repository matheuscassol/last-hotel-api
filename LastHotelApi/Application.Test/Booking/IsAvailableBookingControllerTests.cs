using Application.Controllers;
using Domain.Dtos.Booking;
using Domain.Interfaces.Services.Booking;
using Domain.Models;
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
    public class IsAvailableBookingControllerTests : BookingControllerTests
    {
        [Fact]
        public async Task Should_Return_Ok_Result_When_Service_Is_Able_To_Check_Availability()
        {
            BookingModel.IsAvailable = true;
            _mockMapper.Setup(m => m.Map<BookingModel>(BookingIsAvailableDto)).Returns(BookingModel);
            _mockService.Setup(m => m.IsAvailable(BookingModel)).ReturnsAsync(BookingModel);
            var controller = new BookingsController(_mockService.Object, _mockMapper.Object);
            
            var result = await controller.IsAvailable(BookingIsAvailableDto);

            Assert.True(result is OkObjectResult);
            var resultValue = ((OkObjectResult)result).Value;
            Assert.Equal(BookingModel.IsAvailable, resultValue);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_ModelState_Is_Invalid()
        {
            _mockService.Setup(m => m.IsAvailable(BookingModel)).ReturnsAsync(BookingModel);
            var controller = new BookingsController(_mockService.Object, _mockMapper.Object);
            controller.ModelState.AddModelError("Id", "Id is required");

            var result = await controller.IsAvailable(BookingIsAvailableDto);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Service_Returns_Null()
        {
            _mockMapper.Setup(m => m.Map<BookingModel>(BookingIsAvailableDto)).Returns(BookingModel);
            _mockService.Setup(m => m.IsAvailable(BookingModel)).ReturnsAsync((BookingModel)null);
            var controller = new BookingsController(_mockService.Object, _mockMapper.Object);

            var result = await controller.IsAvailable(BookingIsAvailableDto);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Service_Returns_Invalid_Result()
        {
            var expectedNotification = new Notification("test", "test message");
            BookingModel.AddNotification(expectedNotification);

            _mockMapper.Setup(m => m.Map<BookingModel>(BookingIsAvailableDto)).Returns(BookingModel);
            _mockService.Setup(m => m.IsAvailable(BookingModel)).ReturnsAsync(BookingModel);
            var controller = new BookingsController(_mockService.Object, _mockMapper.Object);

            var result = await controller.IsAvailable(BookingIsAvailableDto);

            Assert.True(result is BadRequestObjectResult);
            Assert.Single(((BadRequestObjectResult)result).Value as IEnumerable<Notification>);
            Assert.Collection(((BadRequestObjectResult)result).Value as IEnumerable<Notification>, item =>
            {
                Assert.Equal(expectedNotification, item);
            });
        }
    }
}
