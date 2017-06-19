//
//  BlueimpLightbox.cs
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

using System.Web.UI;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client.ClientResourceManagement;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.Lightboxes
{
    public class BlueimpLightbox: ILightbox
    {
        public void Register (Page page)
        {
            JavaScript.RequestRegistration ("jQuery-BlueimpGallery");
            ClientResourceManager.RegisterStyleSheet (page, "~/Resources/Libraries/jQuery-BlueimpGallery/02_25_00/css/blueimp-gallery.min.css");
        }

        public string GetLinkAttributes (IImage image, int moduleId)
        {
            return $"{{\"data-gallery\":\"#gallery-{moduleId}\"}}";
        }
    }
}
