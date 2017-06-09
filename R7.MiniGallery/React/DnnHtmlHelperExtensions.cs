using React.Exceptions;
using React.TinyIoC;
using System.Web;
using DotNetNuke.Web.Mvc.Helpers;
using React;

namespace R7.MiniGallery.React
{
    public static class HtmlHelperExtensions
    {
        //
        // Static Properties
        //
        private static IReactEnvironment Environment {
            get {
                IReactEnvironment current;
                try {
                    current = ReactEnvironment.Current;
                }
                catch (TinyIoCResolutionException innerException) {
                    throw new ReactNotInitialisedException ("ReactJS.NET has not been initialised correctly.", innerException);
                }
                return current;
            }
        }

        //
        // Static Methods
        //
        public static IHtmlString React<T> (this DnnHtmlHelper htmlHelper, string componentName, T props, string htmlTag = null, string containerId = null, bool clientOnly = false, bool serverOnly = false, string containerClass = null)
        {
            IHtmlString result;
            try {
                IReactComponent reactComponent = HtmlHelperExtensions.Environment.CreateComponent<T> (componentName, props, containerId, clientOnly);
                if (!string.IsNullOrEmpty (htmlTag)) {
                    reactComponent.ContainerTag = htmlTag;
                }
                if (!string.IsNullOrEmpty (containerClass)) {
                    reactComponent.ContainerClass = containerClass;
                }
                result = new HtmlString (reactComponent.RenderHtml (clientOnly, serverOnly));
            }
            finally {
                HtmlHelperExtensions.Environment.ReturnEngineToPool ();
            }
            return result;
        }

        public static IHtmlString ReactInitJavaScript (this DnnHtmlHelper htmlHelper, bool clientOnly = false)
        {
            IHtmlString result;
            try {
                string initJavaScript = HtmlHelperExtensions.Environment.GetInitJavaScript (clientOnly);
                result = new HtmlString ("<script type=\"text/javascript\">" +
                                         initJavaScript + "</script>");
            }
            finally {
                HtmlHelperExtensions.Environment.ReturnEngineToPool ();
            }
            return result;
        }

        public static IHtmlString ReactWithInit<T> (this DnnHtmlHelper htmlHelper, string componentName, T props, string htmlTag = null, string containerId = null, bool clientOnly = false, string containerClass = null)
        {
            IHtmlString result;
            try {
                IReactComponent reactComponent = HtmlHelperExtensions.Environment.CreateComponent<T> (componentName, props, containerId, clientOnly);
                if (!string.IsNullOrEmpty (htmlTag)) {
                    reactComponent.ContainerTag = htmlTag;
                }
                if (!string.IsNullOrEmpty (containerClass)) {
                    reactComponent.ContainerClass = containerClass;
                }
                string arg_5C_0 = reactComponent.RenderHtml (clientOnly, false);
                string tag = "<script type=\"text/javascript\">" + 
                    reactComponent.RenderJavaScript () + "</script>";
                result = new HtmlString (arg_5C_0 + System.Environment.NewLine + tag);
            }
            finally {
                HtmlHelperExtensions.Environment.ReturnEngineToPool ();
            }
            return result;
        }
    }
}
