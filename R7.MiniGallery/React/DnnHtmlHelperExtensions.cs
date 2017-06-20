using System.Web;
using DotNetNuke.Web.Mvc.Helpers;
using System.Web.Mvc;

namespace R7.MiniGallery.React
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString React (this DnnHtmlHelper htmlHelper, string containerId, object props)
        {
            return ReactRenderer.Render (containerId, props);
        }

        public static IHtmlString RenderReactAssets (this DnnHtmlHelper htmlHelper)
        {
            return ReactRenderer.RenderReactAssets ();
        }
    }
}
