using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Business.Mapping;
using Business.Models;
using DataAccess.Repositories.Gallery;

namespace Business.Gallery
{
    public class AzureGalleryService : IGalleryService<AzureGalleryImageDTO>
    {
        private PhotoRepository photoRepository;

        public AzureGalleryService()
        {
            photoRepository = new PhotoRepository();
        }

        ICollection<AzureGalleryImageDTO> IGalleryService<AzureGalleryImageDTO>.GetGalleryImages()
        {
            var azurePhotos = photoRepository.Items().AsEnumerable().Select(photo => photo.ToGalleryImageDTO()).ToList();
            return azurePhotos;
        }
    }
}
