using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using DotNetNuke.Common;
using R7.DotNetNuke.Extensions.TextExtensions;

namespace R7.MiniGallery.ViewModels
{
    public static class StyleSets
    {
        public static IEnumerable<SelectListItem> Get (string selectedStyleSet)
        {
            return Directory.GetFiles (Path.Combine (Globals.ApplicationMapPath, "DesktopModules", "MVC", "R7.MiniGallery", "css"), "style-*.css").Select (file => {
                var styleSet = Path.GetFileNameWithoutExtension (file).Substring (6).FirstCharToUpper ();
                return new SelectListItem {
                    Value = styleSet,
                    Text = styleSet,
                    Selected = styleSet == selectedStyleSet.ToString ()
                };
            });
        }
    }
}
