using System.Collections.Generic;
using System.Threading;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Localization;
using Newtonsoft.Json;
using R7.MiniGallery.Lightboxes;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.ViewModels
{
    public class MiniGalleryViewModel
    {
        public IList<ImageViewModel> Images { get; set;}

        [JsonIgnore]
        public MiniGallerySettings Settings { get; set; }

        [JsonIgnore]
        public ILightbox Lightbox { get; set; }

        public string LightboxType => Lightbox.GetType ().Name;

        public int TotalImages { get; set; }

        public IDictionary<string,string> ClientResources =>
            LocalizationProvider.Instance.GetCompiledResourceFile (
                PortalSettings.Current, "/DesktopModules/MVC/R7.MiniGallery/App_LocalResources/ClientResources.resx",
                Thread.CurrentThread.CurrentCulture.Name);

        public MiniGalleryClientSettings ClientSettings => new MiniGalleryClientSettings (Settings);
    }
}
