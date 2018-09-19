using System;
using System.Collections.Generic;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Business.Gallery
{
    public class BingGalleryService : IGalleryService<GalleryImageDTO>
    {
        public ICollection<GalleryImageDTO> GetGalleryImages()
        {
            var searchResults = BingImageSearch("puppies");

            var galleryImages = new List<GalleryImageDTO>();

            var responseType = new { value = new[] { new { contentUrl = "", name = "", width = 0, height = 0 } } };
            var results = JsonConvert.DeserializeAnonymousType(searchResults.jsonResult, responseType);

            foreach (var image in results.value)
            {
                galleryImages.Add(new GalleryImageDTO()
                {
                    Url = image.contentUrl,
                    Name = image.name
                });
            }
            return galleryImages;
        }
        struct SearchResult
        {
            public String jsonResult;
            public Dictionary<String, String> relevantHeaders;
        }

        private SearchResult BingImageSearch(string toSearch)
        {
            var uriQuery = Configuration.BingSearchSetting.UriBase + "?q=" + Uri.EscapeDataString(toSearch);

            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = Configuration.BingSearchSetting.AccessKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            // Create the result object for return
            var searchResult = new SearchResult()
            {
                jsonResult = json,
                relevantHeaders = new Dictionary<String, String>()
            };

            // Extract Bing HTTP headers
            foreach (String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.relevantHeaders[header] = response.Headers[header];
            }
            return searchResult;
        }
    }
}
