using System;
using System.Linq;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRepository<Entity> : IDisposable where Entity : class, new()
    {
        Entity CreateNew();
        void Remove(Entity e);
        Entity Find(int id);
        IQueryable<Entity> Items();
        void Update(Entity e);
        void SaveChanges();
    }
}
