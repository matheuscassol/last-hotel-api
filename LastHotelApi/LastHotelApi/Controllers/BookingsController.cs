using AutoMapper;
using Domain.Dtos.Booking;
using Domain.Interfaces.Services.Booking;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : BaseCrudController<BookingModel, BookingPostDto, BookingPostResultDto, BookingPutDto, BookingPutResultDto, BookingGetResultDto>
    {
        private readonly IBookingService _bookingService;
        public BookingsController(IBookingService service, IMapper mapper) : base(service, mapper)
        {
            _bookingService = service;
        }

        [HttpGet]
        [Route("IsAvailable")]
        public async Task<ActionResult> IsAvailable([FromQuery] BookingIsAvailableDto booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<BookingModel>(booking);
            
            var result = await _bookingService.IsAvailable(model);
            if (result == null)
            {
                return BadRequest();
            }
            if (result.IsValid)
            {
                return Ok(result.IsAvailable);
            }
            else
            {
                return BadRequest(result.Notifications);
            }
        }
    }
}
