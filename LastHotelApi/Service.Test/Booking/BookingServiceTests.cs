using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Booking;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Test.Booking
{
    public class BookingServiceTests
    {
        protected Mock<IBookingRepository> _mockRepository = new Mock<IBookingRepository>();
        protected Mock<IClientRepository> _mockClientRepository = new Mock<IClientRepository>();
        protected Mock<IMapper> _mockMapper = new Mock<IMapper>();

        public Guid BookingId { get; set; }
        public List<BookingSelectResultDto> BookingSelectResultDtoList { get; set; } = new List<BookingSelectResultDto>();
        public BookingSelectResultDto BookingSelectResultDto { get; set; }
        public BookingCreateDto BookingCreateDto { get; set; }
        public BookingCreateResultDto BookingCreateResultDto { get; set; }
        public BookingUpdateDto BookingUpdateDto { get; set; }
        public BookingUpdateResultDto BookingUpdateResultDto { get; set; }
        public List<BookingEntity> BookingEntities { get; set; } = new List<BookingEntity>();
        public BookingEntity BookingEntity { get; set; }
        public BookingModel BookingModel { get; set; }
        public BookingInputDto BookingInputDto { get; set; }

        public BookingServiceTests()
        {
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

                var entity = new BookingEntity
                {
                    Id = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                BookingEntities.Add(entity);
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

            BookingEntity = new BookingEntity
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            BookingModel = new BookingModel
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow.AddDays(1).Date,
                EndDate = DateTime.UtcNow.AddDays(4).Date,
                CreatedAt = DateTime.UtcNow,
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
