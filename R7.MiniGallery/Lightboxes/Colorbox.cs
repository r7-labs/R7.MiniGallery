//
//  Colorbox.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014-2017 Roman M. Yagodin
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

using System.Globalization;
using System.Web.UI;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client;
using DotNetNuke.Web.Client.ClientResourceManagement;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.Lightboxes
{
    public class Colorbox: ILightbox
    {
        public void Register (Page page)
        {
            JavaScript.RequestRegistration ("Colorbox");
            RegisterLocalizationScript (page);

            ClientResourceManager.RegisterScript (page, "~/DesktopModules/MVC/R7.MiniGallery/js/lib/colorbox.js");
            ClientResourceManager.RegisterStyleSheet (page, "~/Resources/Libraries/Colorbox/01_06_04/example1/colorbox.css");
        }

        protected void RegisterLocalizationScript (Page page)
        {
            var lang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            if (lang != "en") {
                var library = JavaScriptLibraryController.Instance.GetLibrary (L => L.LibraryName == "Colorbox");
                if (library != null) {
                    ClientResourceManager.RegisterScript (
                        page, $"~/Resources/Libraries/Colorbox/01_06_04/i18n/jquery.colorbox-{GetAvailableScriptLanguageName (lang)}.js",
                        library.PackageID + (int) FileOrder.Js.DefaultPriority + 1,
                        "DnnFormBottomProvider"
                    );
                }
            }
        }

        protected string GetAvailableScriptLanguageName (string lang)
        {
            if (lang == "pt") {
                return "pt-BR";
            }
             
            if (lang == "zh") {
                return "zh-CN";
            }

            return lang;
        }

        public string GetLinkAttributes (IImage image, int moduleId)
        {
            return $"{{\"data-colorbox\":\"gallery-{moduleId}\"}}";
        }
    }
}
