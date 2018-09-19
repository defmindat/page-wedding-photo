using System.Linq;
using DataAccess.Entities.Gallery;
using Business.Models;

namespace Business.Mapping
{
    // dont want to use AutoMapper in so small project and not using 3rd party solutions
    public static class MappingHelper
    {
        public static AzureGalleryImageDTO ToGalleryImageDTO(this Photo photo)
        {
            return new AzureGalleryImageDTO()
            {
                Name = photo.FileName,
                Data = photo.ImageData,
                Annotations = photo.PhotoAnnotations.Select(a => a.ToAnnotationDTO()).ToList()
            };
        }

        public static AnnotationDTO ToAnnotationDTO(this PhotoAnnotation annotation)
        {
            return new AnnotationDTO() {Tag = annotation.Tag};
        }
    }
}
