using System;
using System.Collections.Generic;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace Business.Gallery
{
    public class BingGalleryService : IGalleryService<GalleryImageDTO, SearchParams>
    {
        public IEnumerable<GalleryImageDTO> GetImages(SearchParams parameters)        
        {
            var jsonResult = BingImageSearch(parameters);

            var galleryImages = new List<GalleryImageDTO>();            
            BingImageSearchResponse bingImageSearchResponse = JsonConvert.DeserializeObject<BingImageSearchResponse>(jsonResult);

            if (bingImageSearchResponse == null) return null;

            foreach (var image in bingImageSearchResponse.value)
            {
                galleryImages.Add(new GalleryImageDTO()
                {
                    ID = image.imageId,
                    Url = image.thumbnailUrl, 
                    Name = image.name,
                    Source = GetSourceName()
                });
            }
            return galleryImages;
        }        

        private string BingImageSearch(SearchParams parameters)
        {
            string json = "";
            try
            {
                if (string.IsNullOrEmpty(parameters?.Filter))
                    return string.Empty;

                int offset = parameters.CurrentOffset;
                string queryString = "?q=" + Uri.EscapeDataString(parameters.Filter) + "&count=" + parameters.Count +
                                     "&offset=" + offset;
                var uriQuery = Configuration.BingSearchSetting.UriBase + queryString;

                WebRequest request = WebRequest.Create(uriQuery);
                request.Headers["Ocp-Apim-Subscription-Key"] = Configuration.BingSearchSetting.AccessKey;
                HttpWebResponse response = (HttpWebResponse) request.GetResponseAsync().Result;
                json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                // need to log error info example log4net
            }
            return json;
        }        

        public bool HasAnyImages(SearchParams parameters)
        {
            var jsonResult = BingImageSearch(parameters);
            BingImageSearchResponse bingImageSearchResponse = JsonConvert.DeserializeObject<BingImageSearchResponse>(jsonResult);

            if (bingImageSearchResponse == null) return false;

            bool hasAnyImages = parameters.CurrentOffset < bingImageSearchResponse.totalEstimatedMatches;

            return hasAnyImages;
        }        

        public string GetSourceName()
        {
            return "Bing";
        }
    }    
}
