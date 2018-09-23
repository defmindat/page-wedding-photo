using Business.Gallery;
using Business.Interfaces;
using Business.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Wedding.ViewModels.Gallery;
using Business.Services.SourceManager;
using Business.Helpers;
using System;
using System.Net;

namespace Wedding.Controllers
{
    // PhotoGallery builds completed html for infiniteajaxscroll control
    public class PhotoGalleryController : Controller
    {
        // Service for management photo sources 
        private SourceManagementService<GalleryImageDTO, SearchParams> _sourceManagementService;
               
        public PhotoGalleryController()
        {            
            _sourceManagementService = 
                new SourceManagementService<GalleryImageDTO, SearchParams>(
                    new List<IGalleryService<GalleryImageDTO, SearchParams>>() {
                        new AzureGalleryService(),
                        new BingGalleryService()
                    }
                );
        }

        [HttpGet]
        public ActionResult SearchControl()
        {
            List<string> searchButtons = new List<string>() {"Photographers", "Venues", "Flowers", "Cake", "Invitations", "Rings"};
            
            return View(searchButtons);
        }

        // get completed html
        public ActionResult PhotoSet(int? pageNumber = 1, string filter = "", string currentSource = "")
        {
            if (string.IsNullOrEmpty(filter)) return null;

            var viewModel = GetPhotoSetData(pageNumber, filter, currentSource);
            if (viewModel == null) new HttpStatusCodeResult(HttpStatusCode.NoContent);

            return PartialView(viewModel);
        }    

        // get data for photo set
        private GalleryViewModel GetPhotoSetData(int? pageNumber = 1, string filter = "", string currentSource = "")
        {
            if (pageNumber < 1)
            {
                throw new Exception("Invalid parameters");
            }

            int photosPerPage = Configuration.GallerySettings.ImagesPerPage;
            var seacrhParameters = new SearchParams(pageNumber, photosPerPage, filter, currentSource);

            IGalleryService<GalleryImageDTO, SearchParams> sourceService =
                _sourceManagementService.GetSourceService(seacrhParameters);

            if (sourceService == null) return null;

            var viewModel = new GalleryViewModel()
            {
                Images = sourceService.GetImages(seacrhParameters),
                CurrentPhotoSource = sourceService.GetSourceName(),
                Filter = filter
            };

            return viewModel;
        }
    }       
}