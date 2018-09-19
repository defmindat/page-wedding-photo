using System.Web;
using System.Web.Mvc;
using Business.Gallery;
using Business.Interfaces;
using Business.Models;
using Wedding.ViewModels.Gallery;
using DataAccess.Repositories.Gallery;
using System.IO;
using DataAccess.Entities.Gallery;

namespace Wedding.Controllers
{
    public class HomeController : Controller
    {
        private IGalleryService<AzureGalleryImageDTO> azureGalleryService;
        private IGalleryService<GalleryImageDTO> bingGalleryService;

        public HomeController()
        {
            azureGalleryService = new AzureGalleryService();
            bingGalleryService = new BingGalleryService();
        }
        public ActionResult Index()
        {
            //var azureGallery = azureGalleryService.GetGalleryImages();
            var bingGallery = bingGalleryService.GetGalleryImages();

            var viewModel = new GalleryViewModel()
            {
                //AzureGalleryImages = azureGallery,
                BingGalleryImages = bingGallery
            };

            return View(viewModel);
        }

     
        public ActionResult Upload(HttpPostedFileBase file)
        {
            PhotoRepository photoRepo = new PhotoRepository();
            Photo newPhoto = photoRepo.CreateNew();
            newPhoto.FileName = file.FileName;
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();

                newPhoto.ImageData = array;
            }

            photoRepo.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}