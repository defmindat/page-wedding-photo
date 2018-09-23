using System.Data.Entity;
using DataAccess.Entities.Gallery;
using DataAccess.Entities.PinnedPhotos;

namespace DataAccess.Repositories.Context
{
    public class WeddingContext : DbContext
    {
        public WeddingContext() : base("WeddingContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<WeddingContext>());
        }

        public DbSet<Photo> AzurePhotos { get; set; }
        public DbSet<PinnedPhoto> PinnedPhotos { get; set; }
        public DbSet<PhotoAnnotation> PhotoAnnotations { get; set; }
    }
}
