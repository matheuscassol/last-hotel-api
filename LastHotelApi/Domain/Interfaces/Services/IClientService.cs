using Domain.Dtos;
using Domain.Dtos.Booking;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Client
{
    public interface IClientService
    {
        Task<ClientSelectResultDto> GetById(Guid id);
        Task<IEnumerable<ClientSelectResultDto>> GetAll();
        Task<ClientCreateResultDto> Create(ClientCreateDto client);
        Task<ClientUpdateResultDto> Edit(ClientUpdateDto client);
        Task<bool> Delete(Guid id);
    }
}
