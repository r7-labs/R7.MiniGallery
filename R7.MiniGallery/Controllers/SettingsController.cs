﻿//
//  SettingsController.cs
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

using System.Web.Mvc;
using DotNetNuke.Security;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.Controllers
{
    /// <summary>
    /// The Settings Controller manages the modules settings
    /// </summary>
    [DnnModuleAuthorize (AccessLevel = SecurityAccessLevel.Edit)]
    [DnnHandleError]
    public class SettingsController : DnnController
    {
        [HttpGet]
        public ActionResult Index ()
        {
            var settings = new MiniGallerySettings ();
            // TODO: Load settings
            return View (settings);
        }

        [HttpPost]
        [ValidateInput (false)]
        [global::DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Index (MiniGallerySettings settings)
        {
            // TODO: Update settings
            return RedirectToDefaultRoute ();
        }
    }
}