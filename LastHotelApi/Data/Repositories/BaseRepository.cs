using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly HotelContext _context;
        protected DbSet<T> _dataset;
        public BaseRepository(HotelContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(id));
            if (result == null)
                return false;

            _dataset.Remove(result);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<T> InsertAsync(T item)
        {
            item.Id = Guid.NewGuid();
            item.CreatedAt = DateTime.UtcNow;

            _dataset.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<T> SelectAsync(Guid id)
        {
            return await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            return await _dataset.ToListAsync();
        }

        public async Task<T> UpdateAsync(T item)
        {
            var result = await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(item.Id));
            if (result == null)
                return null;

            item.UpdatedAt = DateTime.UtcNow;
            item.CreatedAt = result.CreatedAt;

            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dataset.AnyAsync(x => x.Id.Equals(id));
        }
    }
}
