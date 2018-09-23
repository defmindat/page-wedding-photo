using System.Collections.Generic;

namespace Business.Models
{
    public class PinnedPhotoDTO
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public List<AnnotationDTO> Annotations { get; set; }
    }
}
