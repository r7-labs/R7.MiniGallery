using System.Web.UI;
using DotNetNuke.Web.Client;
using DotNetNuke.Web.Client.ClientResourceManagement;
using DotNetNuke.Framework.JavaScriptLibraries;
using R7.Dnn.Extensions.Client;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.Lightboxes
{
    public class BlueimpLightbox: ILightbox
    {
        public void Register (Page page)
        {
            JavaScript.RequestRegistration ("jQuery-BlueimpGallery");
            var blueimpLibrary = JavaScriptLibraryHelper.GetHighestVersionLibrary ("jQuery-BlueimpGallery");
            if (blueimpLibrary != null) {
                JavaScriptLibraryHelper.RegisterStyleSheet (blueimpLibrary,
                    page, "css/blueimp-gallery.min.css", (int) FileOrder.Css.SkinCss, "DnnPageHeaderProvider", "blueimp-gallery");
            }
        }

        public string GetLinkAttributes (IImage image, int moduleId)
        {
            return $"{{\"data-gallery\":\"#gallery-{moduleId}\"}}";
        }
    }
}
