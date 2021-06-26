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
    public class GetAllBookingServiceTests: BookingServiceTests
    {
        [Fact]
        public async Task Should_Get_All_Bookings()
        {
            _mockRepository.Setup(m => m.SelectAsync()).ReturnsAsync(BookingEntities);
            _mockMapper.Setup(m => m.Map<IEnumerable<BookingSelectResultDto>>(BookingEntities)).Returns(BookingSelectResultDtoList);
            var service = new BookingService(_mockRepository.Object, _mockMapper.Object, _mockClientRepository.Object);
            
            var result = await service.GetAll();

            Assert.Equal(result, BookingSelectResultDtoList);
        }
    }
}
