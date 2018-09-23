using System;

namespace Business.Models
{
    public class BingImageSearchResponse
    {
        public string _type { get; set; }        
        public int totalEstimatedMatches { get; set; }
        public Value[] value { get; set; }                
        public int nextOffset { get; set; }
    }

    public class Value
    {
        public string name { get; set; }
               
        public string thumbnailUrl { get; set; }
        public DateTime datePublished { get; set; }
        public string contentUrl { get; set; }        
        public string imageId { get; set; }
        public string accentColor { get; set; }
    }
}