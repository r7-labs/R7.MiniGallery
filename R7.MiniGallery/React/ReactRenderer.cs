using System.Web.Mvc;
using Orc.SuperchargedReact.Web;

namespace R7.MiniGallery.React
{
    public static class ReactRenderer
    {
        public static MvcHtmlString Render (string containerId, object props)
        {
            return ReactHtmlExtensions.Render (null, containerId, props);
        }

        public static MvcHtmlString RenderReactAssets ()
        {
            return ReactHtmlExtensions.RenderReactAssets (null);
        }
    }
}
