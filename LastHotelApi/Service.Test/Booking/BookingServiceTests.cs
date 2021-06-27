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
        public BookingModel BookingModel { get; set; }
        public List<BookingModel> BookingModels { get; set; } = new List<BookingModel>();
        public BookingEntity BookingEntity { get; set; }
        public List<BookingEntity> BookingEntities { get; set; } = new List<BookingEntity>();

        public BookingServiceTests()
        {
            BookingId = Guid.NewGuid();

            for (int i = 0; i < 10; i++)
            {
                var model = new BookingModel
                {
                    Id = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow
                };
                BookingModels.Add(model);

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
        }
    }
}
