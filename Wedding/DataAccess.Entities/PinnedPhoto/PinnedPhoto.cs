using System.Collections.Generic;

namespace DataAccess.Entities.PinnedPhotos
{
    public class PinnedPhoto
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public virtual ICollection<PhotoAnnotation> PhotoAnnotations { get; set; }

        public PinnedPhoto()
        {
            PhotoAnnotations = new List<PhotoAnnotation>();
        }
    }
}
