using Domain.Dtos;
using Domain.Dtos.Booking;
using Domain.Entities;
using Domain.Interfaces.Services.Booking;
using Domain.Interfaces.Services.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _bookingService.GetAll());
        }

        [HttpGet]
        [Route("{id}", Name = "GetBookingById")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _bookingService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BookingCreateDto booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookingService.Create(booking);
            if (result == null)
            {
                return BadRequest();
            }
            else if (result.IsValid)
            {
                return Created(new Uri(Url.Link("GetBookingById", new { id = result.Id })), result);
            }
            else
            {
                return BadRequest(result.Notifications);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] BookingUpdateDto booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookingService.Edit(booking);
            if (result == null)
            {
                return BadRequest();
            }
            if (result.IsValid)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Notifications);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _bookingService.Delete(id));
        }

        [HttpGet]
        [Route("IsAvailable")]
        public async Task<ActionResult> IsAvailable([FromQuery] BookingInputDto booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookingService.IsAvailable(booking);
            
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
