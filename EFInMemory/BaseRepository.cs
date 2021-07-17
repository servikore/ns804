using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFInMemory
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MemoryDbContext context { private set; get; }
        protected DbSet<T> entities { private set; get; }

        public BaseRepository(MemoryDbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            return this.entities.ToListAsync();
        }

        public Task<T> GetById(int id)
        {
            return this.entities.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task Insert(T obj)
        {
            return this.entities.AddAsync(obj);
        }
        public Task Update(T obj)
        {
            return this.context.SaveChangesAsync();
        }

        public Task Save()
        {
            return this.context.SaveChangesAsync();
        }        
    }
}
