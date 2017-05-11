﻿//
//  Lightboxes.cs
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
