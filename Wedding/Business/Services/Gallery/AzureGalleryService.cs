using System.Collections.Generic;
using Business.Interfaces;
using Business.Models;
using Business.Helpers;
using DataAccess.Repositories.Gallery;
using System.Linq;
using System;
using DataAccess.Entities.Gallery;

namespace Business.Gallery
{
    public class AzureGalleryService : IGalleryService<GalleryImageDTO, SearchParams>
    {
        private EfRepository<Photo> _photoRepository;
        public AzureGalleryService()
        {
            _photoRepository = new EfRepository<Photo>();
        }

        public IEnumerable<GalleryImageDTO> GetImages(SearchParams parameters)
        {
            // I think we don't need to create protection from SQL Injection. Project is simple
            if (parameters.Start < 1)
            {
                throw new Exception("Invalid parameters");
            }

            int imagesPerPage = Configuration.GallerySettings.ImagesPerPage;
            var blobImages = _photoRepository.Items().Where(p => !string.IsNullOrEmpty(parameters.Filter) && p.FileName.Contains(parameters.Filter)).OrderBy(p => p.FileName).Skip(parameters.CurrentOffset).Take(imagesPerPage);

            List<AzureGalleryImageDTO> images = new List<AzureGalleryImageDTO>();

            foreach (var image in blobImages)
            {
                images.Add(new AzureGalleryImageDTO()
                {
                    ID = image.ID.ToString(),
                    Url = image.BlobUri,
                    Name = image.FileName,
                    Source = GetSourceName()
                });
            }

            return images;
        }

        public bool HasAnyImages(SearchParams parameters)
        {            
            int imagesPerPage = Configuration.GallerySettings.ImagesPerPage;
            bool hasAnyImages = _photoRepository.Items().Where(p => string.IsNullOrEmpty(parameters.Filter) || p.FileName.Contains(parameters.Filter)).OrderBy(p => p.FileName).Skip((parameters.Start != null ? parameters.Start.Value  - 1 : 0) * imagesPerPage).Take(imagesPerPage).Any();
            return hasAnyImages;
        }

        public string GetSourceName()
        {
            return "Azure";
        }
    }
}
