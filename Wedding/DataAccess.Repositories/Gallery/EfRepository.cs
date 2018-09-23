using System.Data.Entity;
using System.Linq;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Context;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace DataAccess.Repositories.Gallery
{
    public class EfRepository<Entity>: IRepository<Entity> where Entity : class, new()
    {
        private bool disposed = false;
        private WeddingContext _dbContext;
        private DbSet<Entity> _dbSet;

        public EfRepository()
        {
            _dbContext = new WeddingContext();
            _dbSet = _dbContext.Set<Entity>();
        }

        public Entity CreateNew()
        {
            Entity Entity = new Entity();
            return _dbSet.Add(Entity);
        }

        public Entity Find(int id)
        {
            if (id > 0)
            {
                return _dbSet.Find(id);
            }

            return null;
        }

        public IQueryable<Entity> Items()
        {
            return _dbSet.AsQueryable();
        }

        public void Remove(Entity Entity)
        {
            if (Entity != null)
            {
                _dbSet.Remove(Entity);
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Entity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
