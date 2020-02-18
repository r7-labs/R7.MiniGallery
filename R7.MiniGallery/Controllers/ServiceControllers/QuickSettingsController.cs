//
//  QuickSettingsController.ascx.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2020 Roman M. Yagodin
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
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.Api;
using R7.MiniGallery.Models;
using R7.MiniGallery.ViewModels;

namespace R7.MiniGallery.Controllers.ServiceControllers
{
    [SupportedModules ("R7.MiniGallery")]
    [DnnModuleAuthorize (AccessLevel = SecurityAccessLevel.Edit)]
    public class QuickSettingsController: DnnApiController
    {
        protected readonly MiniGallerySettingsRepository SettingsRepository = new MiniGallerySettingsRepository ();

        [HttpGet]
        public HttpResponseMessage Get ()
        {
            try {
                var settings = SettingsRepository.GetSettings (ActiveModule);

                var quickSettings = new QuickSettingsViewModel {
                    ImageCssClass = settings.ImageCssClass,
                    NumberOfRecords = settings.NumberOfRecords,
                    ShowTitles = settings.ShowTitles
                };

                return Request.CreateResponse (quickSettings);
            }
            catch (Exception ex) {
                Exceptions.LogException (ex);
                return Request.CreateResponse (new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Update (QuickSettingsViewModel quickSettings)
        {
            try {
                var settings = SettingsRepository.GetSettings (ActiveModule);

                settings.ImageCssClass = quickSettings.ImageCssClass;
                settings.NumberOfRecords = quickSettings.NumberOfRecords;
                settings.ShowTitles = quickSettings.ShowTitles;

                SettingsRepository.SaveSettings (ActiveModule, settings);

                DataCache.ClearCache ("//r7_MiniGallery");
                ModuleController.SynchronizeModule (ActiveModule.ModuleID);

                return Request.CreateResponse (new { success = true });
            }
            catch (Exception ex) {
                Exceptions.LogException (ex);
                return Request.CreateResponse (new { success = false });
            }
        }
    }
}