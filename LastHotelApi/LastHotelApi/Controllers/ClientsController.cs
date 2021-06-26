using Domain.Dtos;
using Domain.Entities;
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
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _clientService.GetAll());
        }

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _clientService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClientCreateDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
 
            var result = await _clientService.Create(client);
            if (result != null)
            {
                return Created(new Uri(Url.Link("GetById", new { id = result.Id })), result);
            } else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ClientUpdateDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _clientService.Edit(client);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
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

            return Ok(await _clientService.Delete(id));
        }
    }
}
