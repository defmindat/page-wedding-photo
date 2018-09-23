using Business.Interfaces;
using Business.Models;
using System.Collections.Generic;

namespace Business.Services.SourceManager
{
    // search data source from different search system
    public class SourceManagementService<Image, Params>
        where Image: GalleryImageDTO
        where Params: SearchParams
    {   
        private IEnumerable<IGalleryService<Image, Params>> _services { get; set; }
        public SourceManagementService(ICollection<IGalleryService<Image, Params>> services)
        {
            _services = services;
        }

        public IGalleryService<Image, Params> GetSourceService(Params parameters)
        {
            foreach(var source in _services)
            {
                bool isCurrentSource = parameters.CurrentSource == source.GetSourceName();
                bool notUsedBefore = string.IsNullOrEmpty(parameters.CurrentSource);

                // it is first loading OR source is current AND has images
                if (notUsedBefore || isCurrentSource)
                {
                    bool hasAnyImages = source.HasAnyImages(parameters);
                    if (hasAnyImages)
                        return source;

                    // skip source, reset start position to 0 and go next
                    ResetSearchParamsToDefault(parameters);
                }               
            }
            return null;
        }

        private void ResetSearchParamsToDefault(Params parameters)
        {
            parameters.Start = 1;
            parameters.CurrentSource = string.Empty;
        }
    }
}
