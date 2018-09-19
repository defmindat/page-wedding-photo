using System.Collections.Generic;

namespace DataAccess.Entities.Gallery
{
    public class Photo
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public byte[] ImageData { get; set; }
        public virtual ICollection<PhotoAnnotation> PhotoAnnotations { get; set; }
    }
}
