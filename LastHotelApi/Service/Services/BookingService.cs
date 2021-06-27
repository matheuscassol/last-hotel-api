using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Booking;
using Domain.Models;
using Service.Notifications;
using System;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BookingService : BaseCrudService<BookingModel, BookingEntity>, IBookingService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository, IMapper mapper, IClientRepository clientRepository) : base(bookingRepository, mapper)
        {
            _clientRepository = clientRepository;
            _bookingRepository = bookingRepository;
        }
        override protected async Task ValidateModel(BookingModel model)
        {
            await ValidateClientId(model);

            ValidateStartDatePriorToEndDate(model);

            ValidateBookingPeriod(model);

            ValidateDaysInAdvance(model);

            await ValidateAvailability(model);
        }

        private async Task ValidateAvailability(BookingModel model)
        {
            await CheckAvailability(model);
            if (!model.IsAvailable)
            {
                model.AddNotification(BookingNotifications.NotAvailable);
            }
        }

        private static void ValidateDaysInAdvance(BookingModel model)
        {
            var daysInAdvance = model.StartDate.Subtract(DateTime.UtcNow.Date).TotalDays;
            if (daysInAdvance < 1 || daysInAdvance > 30)
            {
                model.AddNotification(BookingNotifications.InvalidDate);
            }
        }

        private static void ValidateBookingPeriod(BookingModel model)
        {
            //Adding 1 second to count how many days
            var bookingDays = model.EndDate.AddSeconds(1).Subtract(model.StartDate).TotalDays;
            if (bookingDays > 3)
            {
                model.AddNotification(BookingNotifications.InvalidPeriod);
            }
        }

        private static void ValidateStartDatePriorToEndDate(BookingModel model)
        {
            if (model.StartDate >= model.EndDate)
            {
                model.AddNotification(BookingNotifications.InvertedDates);
            }
        }

        private async Task ValidateClientId(BookingModel model)
        {
            if (!await _clientRepository.ExistsAsync(model.ClientId))
            {
                model.AddNotification(BookingNotifications.InvalidClient);
            }
        }

        public async Task<BookingModel> IsAvailable(BookingModel model)
        {
            ValidateStartDatePriorToEndDate(model);
            if (model.IsValid)
            {
                await CheckAvailability(model);
            }

            return model;
        }

        private async Task CheckAvailability(BookingModel model)
        {
            var entity = _mapper.Map<BookingEntity>(model);
            model.IsAvailable = await _bookingRepository.IsAvailable(entity);
        }
    }
}
