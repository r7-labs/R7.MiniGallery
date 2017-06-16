using System.Web;
using DotNetNuke.Web.Mvc.Helpers;

namespace R7.MiniGallery.React
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString React<T> (this DnnHtmlHelper htmlHelper, string componentName, T props, string htmlTag = null, string containerId = null, bool clientOnly = false, bool serverOnly = false, string containerClass = null)
        {
            return ReactRenderer.React (componentName, props, htmlTag, containerId, clientOnly, serverOnly, containerClass);
        }

        public static IHtmlString ReactInitJavaScript (this DnnHtmlHelper htmlHelper, bool clientOnly = false)
        {
            return ReactRenderer.ReactInitJavaScript (clientOnly);
        }

        public static IHtmlString ReactWithInit<T> (this DnnHtmlHelper htmlHelper, string componentName, T props, string htmlTag = null, string containerId = null, bool clientOnly = false, string containerClass = null)
        {
            return ReactRenderer.ReactWithInit (componentName, props, htmlTag, containerId, clientOnly, containerClass);
        }
    }
}
