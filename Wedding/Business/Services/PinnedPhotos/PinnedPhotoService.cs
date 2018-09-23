using System.Collections.Generic;
using System.Linq;
using Business.Models;
using DataAccess.Entities.PinnedPhotos;
using DataAccess.Repositories.Gallery;
namespace Business.Services.PinnedPhotos
{
    public class PinnedPhotoService
    {
        private EfRepository<PinnedPhoto> _pinnedPhotoRepository;
        private EfRepository<PhotoAnnotation> _photoAnnotaionRepository;

        public PinnedPhotoService()
        {
            _pinnedPhotoRepository = new EfRepository<PinnedPhoto>();
            _photoAnnotaionRepository = new EfRepository<PhotoAnnotation>();
        }

        public void SavePhoto(PinnedPhoto photo)
        {
            var pinnedPhoto = _pinnedPhotoRepository.Find(photo.ID);

            if (pinnedPhoto == null)
            {
                pinnedPhoto = _pinnedPhotoRepository.CreateNew();
            }

            pinnedPhoto.Url = photo.Url;
            _pinnedPhotoRepository.SaveChanges();
        }

        public void SaveAnnotations(int photoId, List<string> annotations)
        {
            var listOfExistingAnnotations = _photoAnnotaionRepository.Items().Where(a => a.PinnedPhotoID == photoId).ToList();
            
            foreach (var annotation in listOfExistingAnnotations)
            {
                _photoAnnotaionRepository.Remove(annotation);
            }

            for (int i = 0; i < annotations.Count; i++)
            {
                var photoAnnotation = _photoAnnotaionRepository.CreateNew();
                photoAnnotation.Tag = annotations[i];
                photoAnnotation.PinnedPhotoID = photoId;
            }

            _photoAnnotaionRepository.SaveChanges();
        }

        public List<PinnedPhotoDTO> GetPinnedPhotos()
        {
            var allPinnedPhotos = _pinnedPhotoRepository.Items().ToList();

            List<PinnedPhotoDTO> pinnedPhotosDTOList = allPinnedPhotos.Select(p => new PinnedPhotoDTO()
            {
                ID = p.ID,
                Url = p.Url,
                Annotations = p.PhotoAnnotations.Select(a => new AnnotationDTO()
                {
                    ID = a.ID,
                    Tag = a.Tag,
                    PinnedPhotoID = a.PinnedPhotoID
                }).ToList()
            }).ToList();

            return pinnedPhotosDTOList;
        }

        
    }
}
