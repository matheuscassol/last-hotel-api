using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICrudService<T> where T: Notifiable<Notification>
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T model);
        Task<T> Edit(T model);
        Task<bool> Delete(Guid id);
    }
}
