using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotNetNuke.Services.Localization;
using R7.MiniGallery.Lightboxes;

namespace R7.MiniGallery.ViewModels
{
    public static class Lightboxes
    {
        public static IEnumerable<SelectListItem> Get (LightboxType selectedLightbox, string localResourceFile)
        {
            return Enum.GetNames (typeof (LightboxType)).Select (lt => new SelectListItem {
                Value = lt,
                Text = Localization.GetString ("LightboxType." + lt, localResourceFile),
                Selected = lt == selectedLightbox.ToString ()
            });
        }
    }
}
