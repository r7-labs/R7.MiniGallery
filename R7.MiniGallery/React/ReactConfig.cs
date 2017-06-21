using R7.MiniGallery.React;

[assembly: WebActivatorEx.PreApplicationStartMethod (typeof (R7.MiniGallery.React.ReactConfig), "Configure")]

namespace R7.MiniGallery.React
{
    public static class ReactConfig
    {
        public static void Configure()
        {
            DnnReact.ConfigureOnce ();

            DnnReact.AddScriptWithoutTransform ("~/DesktopModules/MVC/R7.MiniGallery/js/lib/minigallery.js");
            DnnReact.AddScriptWithoutTransform ("~/DesktopModules/MVC/R7.MiniGallery/js/lib/Hello.js");
        }
    }
}
