using System.Collections.Generic;
using Business.Models;

namespace Business.Interfaces
{
    public interface IGalleryService<Image> where Image : GalleryImageDTO 
    {
        ICollection<Image> GetGalleryImages();
    }
}
