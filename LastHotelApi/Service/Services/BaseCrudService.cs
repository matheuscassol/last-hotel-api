using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public abstract class BaseCrudService<Model, Entity> : ICrudService<Model> 
        where Model : Notifiable<Notification> 
        where Entity : BaseEntity
    {
        protected readonly IRepository<Entity> _repository;
        protected readonly IMapper _mapper;

        protected abstract Task ValidateModel(Model model);
        public BaseCrudService(IRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<Model> GetById(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<Model>(entity);
        }

        public async Task<IEnumerable<Model>> GetAll()
        {
            var entities = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<Model>>(entities);
        }

        public async Task<Model> Create(Model model)
        {
            await ValidateModel(model);

            if (!model.IsValid)
            {
                return model;
            }

            var inputEntity = _mapper.Map<Entity>(model);
            var outputEntity = await _repository.InsertAsync(inputEntity);

            return _mapper.Map<Model>(outputEntity);
        }

        public async Task<Model> Edit(Model model)
        {
            await ValidateModel(model);

            if (!model.IsValid)
            {
                return model;
            }

            var inputEntity = _mapper.Map<Entity>(model);
            var outputEntity = await _repository.UpdateAsync(inputEntity);

            return _mapper.Map<Model>(outputEntity);
        }
    }
}
