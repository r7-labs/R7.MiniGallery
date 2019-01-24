//
//  ImageController.cs
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

using System.Linq;
using System.Web.Mvc;
using DotNetNuke.Entities.Icons;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using R7.MiniGallery.Data;
using R7.MiniGallery.Lightboxes;
using R7.MiniGallery.Models;
using R7.MiniGallery.ViewModels;

namespace R7.MiniGallery.Controllers
{
    [DnnHandleError]
    public class ImageController : DnnController
    {
        protected MiniGallerySettingsRepository SettingsRepository;

        public ImageController ()
        {
            SettingsRepository = new MiniGallerySettingsRepository ();
        }

        [ModuleActionItems]
        public ActionResult Index ()
        {
            var settings = SettingsRepository.GetSettings (ActiveModule);
            var lightbox = LightboxFactory.Create (settings.LightboxType);

            var images = ImageViewModelRepository.Instance.GetImages (ModuleContext,
                                                                      settings,
                                                                      lightbox,
                                                                      true,
                                                                      HttpContext.Timestamp,
                                                                      out int totalImages).ToList ();

            var index = 0;
            foreach (var image in images) {
                if (index >= settings.NumberOfRecords) {
                    image.IsHidden = true;
                }
                index++;
            }

            var viewModel = new MiniGalleryViewModel {
                Images = images,
                TotalImages = totalImages,
                Settings = settings,
                Lightbox = lightbox
            };

            return View (viewModel);
        }

        public ModuleActionCollection GetIndexActions ()
        {
            var actions = new ModuleActionCollection ();

            actions.Add (new ModuleAction (-1) {
                CommandName = ModuleActionType.AddContent,
                CommandArgument = string.Empty,
                Title = LocalizeString ("AddImage.Text"),
                Icon = IconController.IconURL ("Add"),
                Url = ModuleContext.EditUrl ("Edit"),
                UseActionEvent = false,
                Secure = SecurityAccessLevel.Edit,
                Visible = true,
                NewWindow = false
            });

            actions.Add (new ModuleAction (-1) {
                Title = LocalizeString ("BulkAddImages.Text"),
                CommandName = ModuleActionType.AddContent,
                CommandArgument = string.Empty,
                Icon = IconController.IconURL ("Add"),
                Url = ModuleContext.EditUrl ("BulkAdd"),
                UseActionEvent = false,
                Secure = SecurityAccessLevel.Edit,
                Visible = true,
                NewWindow = false
            });

            return actions;
        }
    }
}