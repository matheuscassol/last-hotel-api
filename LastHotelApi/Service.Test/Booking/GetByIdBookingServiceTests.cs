using AutoMapper;
using Domain.Dtos.Booking;
using Domain.Interfaces.Repositories;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Test.Booking
{
    public class GetByIdBookingServiceTests: BookingServiceTests
    {
        [Fact]
        public async Task Should_Get_Booking_By_Id()
        {
            _mockRepository.Setup(m => m.SelectAsync(BookingId)).ReturnsAsync(BookingEntity);
            _mockMapper.Setup(m => m.Map<BookingSelectResultDto>(BookingEntity)).Returns(BookingSelectResultDto);
            var service = new BookingService(_mockRepository.Object, _mockMapper.Object, _mockClientRepository.Object);
            
            var result = await service.GetById(BookingId);

            Assert.Equal(result, BookingSelectResultDto);
        }
    }
}
