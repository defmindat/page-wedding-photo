using System.Collections.Generic;
using Business.Models;

namespace Business.Interfaces
{
    public interface IGalleryService<Image, Params> 
        where Image : GalleryImageDTO
        where Params : SearchParams
    {
        IEnumerable<Image> GetImages(Params parameters);
        bool HasAnyImages(Params parameters);
        string GetSourceName();
    }    
}
