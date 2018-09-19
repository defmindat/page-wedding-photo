using System;
using System.Configuration;

namespace Business.Helpers
{
    public static class Configuration
    {
        //public static string ConnectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;

        public static class BingSearchSetting
        {
            public static string AccessKey = ConfigurationManager.AppSettings["BingSearchSetting.AccessKey"];
            public static string UriBase = ConfigurationManager.AppSettings["BingSearchSetting.UriBase"];
        }

    }

}
