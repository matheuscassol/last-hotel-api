using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Client;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ClientSelectResultDto> GetById(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<ClientSelectResultDto>(entity);
        }

        public async Task<IEnumerable<ClientSelectResultDto>> GetAll()
        {
            var entities = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<ClientSelectResultDto>>(entities);
        }

        public async Task<ClientCreateResultDto> Create(ClientCreateDto client)
        {
            var model = _mapper.Map<ClientModel>(client);
            var entity = _mapper.Map<ClientEntity>(model);

            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<ClientCreateResultDto>(result);
        }

        public async Task<ClientUpdateResultDto> Edit(ClientUpdateDto client)
        {
            var model = _mapper.Map<ClientModel>(client);
            var entity = _mapper.Map<ClientEntity>(model);

            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<ClientUpdateResultDto>(result);
        }
    }
}
