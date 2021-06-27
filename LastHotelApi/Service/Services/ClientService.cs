using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Client;
using Domain.Models;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ClientService : BaseCrudService<ClientModel, ClientEntity>, IClientService
    {
        public ClientService(IClientRepository repository, IMapper mapper) : base(repository, mapper)
        {

        }

        protected override Task ValidateModel(ClientModel model)
        {
            //This service didn't really need this method, but in a real scenario there would be async validations for Clients too. ex: Checking if the email is already used
            //If that was not the case, this service would not need to inherit the implementation from BaseCrudService
            return Task.FromResult(default(object));
        }
    }
}
