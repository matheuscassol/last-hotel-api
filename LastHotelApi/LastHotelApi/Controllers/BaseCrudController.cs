using AutoMapper;
using Domain.Dtos;
using Domain.Interfaces.Services;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public abstract class BaseCrudController<Model, PostDto, PostResultDto, PutDto, PutResultDto, GetResultDto> : ControllerBase 
        where Model : Notifiable<Notification>
        where PostResultDto : BasePostResultDto
        where PutResultDto : BasePutResultDto
    {
        private readonly ICrudService<Model> _service;
        protected readonly IMapper _mapper;
        public BaseCrudController(ICrudService<Model> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await _service.GetAll();
            var dto = _mapper.Map<IEnumerable<GetResultDto>>(model);

            return Ok(dto);
        }

        [HttpGet]
        [Route("{id}", Name = "[controller]GetById")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.GetById(id);
            if(result != null)
            {
                var dto = _mapper.Map<GetResultDto>(result);
                return Ok(dto);
            }
            else
            {
                return BadRequest("Could not find record. Please check if the Id is correct");
            }


        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<Model>(item);

            var result = await _service.Create(model);
            if (!result.IsValid)
            {
                return BadRequest(result.Notifications);
            }
            else
            {
                var dto = _mapper.Map<PostResultDto>(result);
                return Created(new Uri(Url.Link($"{ControllerContext.ActionDescriptor?.ControllerName}GetById", new { id = dto.Id })), dto);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PutDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<Model>(item);

            var result = await _service.Edit(model);
            if (result == null)
            {
                return BadRequest("Could not update record because it does not exist. Please check if the Id is correct");
            }
            if (!result.IsValid)
            {
                return BadRequest(result.Notifications);
            }
            else
            {
                var dto = _mapper.Map<PutResultDto>(result);
                return Ok(dto);
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

            var result = await _service.Delete(id);
            
            if(result)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest("Could not delete record because it does not exist. Please check if the Id is correct");
            }
        }
    }
}