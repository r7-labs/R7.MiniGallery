//
//  SettingsController.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017-2019 Roman M. Yagodin
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
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using R7.MiniGallery.Models;
using DnnValidateAntiForgeryTokenAttribute = DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryTokenAttribute;

namespace R7.MiniGallery.Controllers
{
    /// <summary>
    /// The Settings Controller manages the modules settings
    /// </summary>
    [DnnModuleAuthorize (AccessLevel = SecurityAccessLevel.Edit)]
    [DnnHandleError]
    public class SettingsController : DnnController
    {
        protected MiniGallerySettingsRepository SettingsRepository;
        
        public SettingsController ()
        {
            SettingsRepository = new MiniGallerySettingsRepository ();
        }

        [HttpGet]
        public ActionResult Index ()
        {
            var settings = SettingsRepository.GetSettings (ActiveModule);
            return View (settings);
        }

        [HttpPost]
        [ValidateInput (false)]
        [DnnValidateAntiForgeryToken]
        public ActionResult Index (MiniGallerySettings settings)
        {
            // FIXME: https://dnntracker.atlassian.net/browse/DNN-9818

            if (settings.Columns < 0) {
                settings.Columns = 0;
            }

            if (settings.NumberOfRecords < 0) {
                settings.NumberOfRecords = 0;
            }

            if (settings.ThumbWidth < 0) {
                settings.ThumbWidth = 0;
            }

            if (settings.ThumbHeight < 0) {
                settings.ThumbHeight = 0;
            }

            SettingsRepository.SaveSettings (ActiveModule, settings);
            DataCache.ClearCache ("//r7_MiniGallery");
            ModuleController.SynchronizeModule (ModuleContext.ModuleId);

            return RedirectToDefaultRoute ();
        }
    }
}
