using System.Collections.Generic;
using Business.Models;

namespace Wedding.ViewModels.Gallery
{
    public class GalleryViewModel
    {
        public IEnumerable<GalleryImageDTO> Images { get; set; }
        public string CurrentPhotoSource { get;set; }
        public string Filter { get; set; }
    }
}