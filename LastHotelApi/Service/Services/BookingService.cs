using AutoMapper;
using Domain.Dtos.Booking;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Booking;
using Domain.Models;
using Service.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public BookingService(IBookingRepository bookingRepository, IMapper mapper, IClientRepository clientRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _clientRepository = clientRepository;
        }
        public async Task<BookingCreateResultDto> Create(BookingCreateDto booking)
        {
            var model = _mapper.Map<BookingModel>(booking);
            await ValidateModel(model);

            if (!model.IsValid)
            {
                return _mapper.Map<BookingCreateResultDto>(model);
            }

            var inputEntity = _mapper.Map<BookingEntity>(model);
            var outputEntity = await _bookingRepository.InsertAsync(inputEntity);

            return _mapper.Map<BookingCreateResultDto>(outputEntity);
        }

        private async Task ValidateModel(BookingModel model)
        {
            await ValidateClientId(model);

            ValidateStartDatePriorToEndDate(model);

            ValidateBookingPeriod(model);

            ValidateDaysInAdvance(model);

            await ValidateAvailability(model);
        }

        private async Task ValidateAvailability(BookingModel model)
        {
            if (!await IsAvailable(model))
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

        private async Task<bool> IsAvailable(BookingModel model)
        {
            var entity = _mapper.Map<BookingEntity>(model);
            return await _bookingRepository.IsAvailable(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _bookingRepository.DeleteAsync(id);
        }

        public async Task<BookingUpdateResultDto> Edit(BookingUpdateDto booking)
        {
            var model = _mapper.Map<BookingModel>(booking);
            await ValidateModel(model);

            if (!model.IsValid)
            {
                return _mapper.Map<BookingUpdateResultDto>(model);
            }

            var inputEntity = _mapper.Map<BookingEntity>(model);
            var outputEntity = await _bookingRepository.UpdateAsync(inputEntity);

            return _mapper.Map<BookingUpdateResultDto>(outputEntity);
        }

        public async Task<BookingSelectResultDto> GetById(Guid id)
        {
            var entity = await _bookingRepository.SelectAsync(id);
            return _mapper.Map<BookingSelectResultDto>(entity);
        }

        public async Task<IEnumerable<BookingSelectResultDto>> GetAll()
        {
            var entities = await _bookingRepository.SelectAsync();
            return _mapper.Map<IEnumerable<BookingSelectResultDto>>(entities);
        }

        public async Task<BookingAvailableResultDto> IsAvailable(BookingInputDto booking)
        {
            var model = _mapper.Map<BookingModel>(booking);
            ValidateStartDatePriorToEndDate(model);

            var result = new BookingAvailableResultDto(await IsAvailable(model));
            result.AddNotifications(model.Notifications);

            return result;
        }
    }
}
