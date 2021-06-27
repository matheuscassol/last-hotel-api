using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Services.Client;
using Domain.Models;
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
    public class ClientsController : BaseCrudController<ClientModel, ClientPostDto, ClientPostResultDto, ClientPutDto, ClientPutResultDto, ClientGetResultDto>
    {
        public ClientsController(IClientService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
