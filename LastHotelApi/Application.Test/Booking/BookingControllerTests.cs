using AutoMapper;
using Domain.Dtos.Booking;
using Domain.Entities;
using Domain.Interfaces.Services.Booking;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Test.Booking
{
    public abstract class BookingControllerTests
    {
        protected Mock<IBookingService> _mockService = new Mock<IBookingService>();
        protected Mock<IUrlHelper> _mockUrl = new Mock<IUrlHelper>();
        protected Mock<IMapper> _mockMapper = new Mock<IMapper>();
        
        public BookingIsAvailableDto BookingIsAvailableDto { get; set; }
        public BookingModel BookingModel { get; set; }

        public BookingControllerTests()
        {
            _mockUrl.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            BookingIsAvailableDto = new BookingIsAvailableDto
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow
            };

            BookingModel = new BookingModel
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
