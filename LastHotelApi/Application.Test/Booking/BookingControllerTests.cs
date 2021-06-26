using Domain.Dtos.Booking;
using Domain.Entities;
using Domain.Interfaces.Services.Booking;
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

        public Guid BookingId { get; set; }
        public List<BookingSelectResultDto> BookingSelectResultDtoList { get; set; } = new List<BookingSelectResultDto>();
        public BookingSelectResultDto BookingSelectResultDto { get; set; }
        public BookingCreateDto BookingCreateDto { get; set; }
        public BookingCreateResultDto BookingCreateResultDto { get; set; }
        public BookingUpdateDto BookingUpdateDto { get; set; }
        public BookingUpdateResultDto BookingUpdateResultDto { get; set; }
        public BookingInputDto BookingInputDto { get; set; }

        public BookingControllerTests()
        {
            _mockUrl.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            BookingId = Guid.NewGuid();

            for (int i = 0; i < 10; i++)
            {
                var dto = new BookingSelectResultDto
                {
                    Id = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow
                };
                BookingSelectResultDtoList.Add(dto);
            }

            BookingSelectResultDto = new BookingSelectResultDto
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow
            };

            BookingCreateDto = new BookingCreateDto
            {
                ClientId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow
            };

            BookingCreateResultDto = new BookingCreateResultDto
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            BookingUpdateDto = new BookingUpdateDto
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow
            };

            BookingUpdateResultDto = new BookingUpdateResultDto
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            BookingInputDto = new BookingInputDto
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow
            };

            BookingCreateResultDto = new BookingCreateResultDto
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
