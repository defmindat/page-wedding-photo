using System;
using System.Configuration;

namespace Business.Helpers
{
    public static class Configuration
    {
        public static class BingSearchSetting
        {
            public static string AccessKey = ConfigurationManager.AppSettings["BingSearchSetting.AccessKey"];
            public static string UriBase = ConfigurationManager.AppSettings["BingSearchSetting.UriBase"];
        }

        public static class AzureSetting
        {
            public static string StorageConnectionString = ConfigurationManager.AppSettings["AzureSetting.StorageConnectionString"];
            public static string BlobContainer = ConfigurationManager.AppSettings["AzureSetting.BlobContainer"];
        }

        public static class GallerySettings
        {
            public static int ImagesPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["GallerySettings.ImagesPerPage"]);
        }

    }

}
