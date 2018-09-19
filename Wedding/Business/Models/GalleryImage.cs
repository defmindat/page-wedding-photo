using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class GalleryImageDTO
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string Url { get; set; }

        public string ImageBase64String
        {
            get { return "data:image/png;base64," + Convert.ToBase64String(Data, 0, Data?.Length ?? 0); }
        }
    }

    public class AzureGalleryImageDTO: GalleryImageDTO
    {
        public ICollection<AnnotationDTO> Annotations { get; set; }
    }
}
