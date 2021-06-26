using Application.Controllers;
using Domain.Interfaces.Services.Booking;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booking
{
    public class DeleteBookingControllerTests: BookingControllerTests
    {
        [Fact]
        public async Task Should_Return_Ok_When_Service_Is_Able_To_Delete()
        {
            _mockService.Setup(m => m.Delete(BookingId)).ReturnsAsync(true);
            var controller = new BookingsController(_mockService.Object);

            var result = await controller.Delete(BookingId);

            Assert.True(result is OkObjectResult);
            Assert.Equal(true, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_ModelState_Is_Invalid()
        {
            _mockService.Setup(m => m.Delete(BookingId)).ReturnsAsync(true);

            var controller = new BookingsController(_mockService.Object);
            controller.ModelState.AddModelError("Id", "Id is required");

            var result = await controller.Delete(BookingId);

            Assert.True(result is BadRequestObjectResult);
        }
    }
}
