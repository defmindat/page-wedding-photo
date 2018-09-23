using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Business.Services.PinnedPhotos;
using DataAccess.Entities.PinnedPhotos;

namespace Wedding.Controllers
{
    // saved photos controller
    public class PinnedPhotosController : Controller
    {
        private PinnedPhotoService _pinnedPhotoService;

        public PinnedPhotosController()
        {
            _pinnedPhotoService = new PinnedPhotoService();
        }
        public ActionResult Index()
        {
            var pinnedPhotos = _pinnedPhotoService.GetPinnedPhotos();
            return View(pinnedPhotos);

        }
        public ActionResult SaveImage(int? id, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("No image to save");
            }

            PinnedPhoto photo = new PinnedPhoto(){ ID = id ?? 0, Url = url};
            _pinnedPhotoService.SavePhoto(photo);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult SaveAnnotations(int photoId, string annotations)
        {
            if (photoId < 0)
            {
                throw new Exception("No image to save");
            }
            List<string> listOfAnnotations = annotations.Split(',').Select(s => s).ToList();
            _pinnedPhotoService.SaveAnnotations(photoId, listOfAnnotations);
            
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}