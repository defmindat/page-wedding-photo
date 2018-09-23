using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class GalleryImageDTO
    {        
        public string ID { get; set; }
        public string Url { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }        
    }

    public class AzureGalleryImageDTO: GalleryImageDTO
    {
        public ICollection<AnnotationDTO> Annotations { get; set; }
    }
}
