//
//  RouteMapper.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using DotNetNuke.Web.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Orc.SuperchargedReact.Web;
using R7.MiniGallery.React;

namespace R7.MiniGallery
{
    public class RouteMapper : IServiceRouteMapper
	{
		public void RegisterRoutes (IMapRoute mapRouteManager)
		{
			mapRouteManager.MapHttpRoute ("R7.MiniGallery", "MiniGalleryMap1", "{controller}/{action}", null, null, new [] { "R7.MiniGallery.Api" });

            var config = new ReactConfiguration {
                EnableFileWatcher = false,
                EnableCompilation = false,
                DisableGlobalMembers = true,
                SerializerSettings = new JsonSerializerSettings {
                    StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                    ContractResolver = new CamelCasePropertyNamesContractResolver ()
                }
            };

            DnnReact.AddScript ("MiniGallery", "~/DesktopModules/MVC/R7.MiniGallery/js/lib/minigallery.js", config);
		}
	}
}