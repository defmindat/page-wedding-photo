using System.Collections.Generic;
using Business.Models;

namespace Wedding.ViewModels.Gallery
{
    public class GalleryViewModel
    {
        public ICollection<AzureGalleryImageDTO> AzureGalleryImages { get; set; }
        public ICollection<GalleryImageDTO> BingGalleryImages { get; set; }
    }
}