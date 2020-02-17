using DotNetNuke.Web.Api;

namespace R7.MiniGallery.Services
{
    /// <summary>
    /// The ServiceRouteMapper tells the DNN Web API Framework what routes this module uses
    /// </summary>
    public class ServiceRouteMapper: IServiceRouteMapper
    {
        public void RegisterRoutes (IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute ("R7.MiniGallery", "default", "{controller}/{action}", new [] { "R7.MiniGallery.Services" });
        }
    }
}
