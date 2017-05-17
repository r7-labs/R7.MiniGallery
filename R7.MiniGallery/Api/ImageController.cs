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

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using DotNetNuke.UI.Modules;
using DotNetNuke.Web.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using R7.MiniGallery.Data;
using R7.MiniGallery.Lightboxes;
using R7.MiniGallery.Models;
using R7.MiniGallery.ViewModels;

namespace R7.MiniGallery.Api
{
    public class ImageController : DnnApiController
    {
        protected MiniGallerySettingsRepository SettingsRepository;

        protected static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver ()
        };

        public ImageController ()
        {
            SettingsRepository = new MiniGallerySettingsRepository ();
        }

        [HttpGet]
        [AllowAnonymous]
        [SupportedModules ("R7.MiniGallery")]
        public JsonResult<List<ImageViewModel>> GetAll ()
        {
            #if DEBUG
            System.Threading.Thread.Sleep (1000);
            #endif

            var moduleContext = new ModuleInstanceContext { Configuration = ActiveModule };
            var settings = SettingsRepository.GetSettings (ActiveModule);
            var lightbox = LightboxFactory.Create (settings.LightboxType);

            int totalImages;
            var images = ImageViewModelRepository.Instance.GetImages (moduleContext, settings, lightbox, true, Request.GetHttpContext ().Timestamp, out totalImages);

            return Json (images.ToList (), SerializerSettings);
        }
    }
}
