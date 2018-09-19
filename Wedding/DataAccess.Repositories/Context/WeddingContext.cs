using System.Data.Entity;
using DataAccess.Entities.Gallery;

namespace DataAccess.Repositories.Context
{
    public class WeddingContext : DbContext
    {
        public WeddingContext() : base("WeddingContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<WeddingContext>());
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoAnnotation> PhotoAnnotations { get; set; }
    }
}
