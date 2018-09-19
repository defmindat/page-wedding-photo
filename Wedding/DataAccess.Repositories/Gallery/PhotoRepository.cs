using System.Linq;
using DataAccess.Repositories.Context;
using DataAccess.Repositories.Interfaces;
using Photo = DataAccess.Entities.Gallery.Photo;
using System.Data.Entity;

namespace DataAccess.Repositories.Gallery
{
    public class PhotoRepository : IRepository<Photo>
    {
        private bool disposed = false;
        private WeddingContext _dbContext { get; set; }

        public PhotoRepository()
        {
            _dbContext = new WeddingContext();
        }
        public Photo CreateNew()
        {
            Photo photo = new Photo();
            return _dbContext.Photos.Add(photo);
        }

        public Photo Find(int id)
        {
            if (id > 0)
            {
                return _dbContext.Photos.Find(id);
            }

            return null;
        }

        public IQueryable<Photo> Items()
        {
            return _dbContext.Photos.AsQueryable();
        }

        public void Remove(Photo photo)
        {
            if (photo != null)
            {
                _dbContext.Photos.Remove(photo);
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Photo photo)
        {
            _dbContext.Entry(photo).State = EntityState.Modified;
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
