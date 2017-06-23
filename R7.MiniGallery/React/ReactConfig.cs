using R7.Dnn.Extensions.React;

[assembly: WebActivatorEx.PreApplicationStartMethod (typeof (R7.MiniGallery.React.ReactConfig), "Configure")]

namespace R7.MiniGallery.React
{
    public static class ReactConfig
    {
        const string scriptsPath = "~/DesktopModules/MVC/R7.MiniGallery/js/lib/";

        public static void Configure()
        {
            DnnReact.AddScriptsWithoutTransform (
                scriptsPath + "minigallery.js",
                scriptsPath + "Hello.js"
            );
        }
    }
}
