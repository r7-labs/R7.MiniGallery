//
//  ImageViewModelRepository.cs
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
using DotNetNuke.Common.Utilities;
using DotNetNuke.UI.Modules;
using R7.MiniGallery.Lightboxes;
using R7.MiniGallery.Models;
using R7.MiniGallery.ViewModels;

namespace R7.MiniGallery.Data
{
    public class ImageViewModelRepository
    {
        static Lazy<ImageViewModelRepository> _instance = new Lazy<ImageViewModelRepository> ();

        public static ImageViewModelRepository Instance {
            get { return _instance.Value; }
        }

        public IEnumerable<ImageViewModel> GetImages (ModuleInstanceContext moduleContext, MiniGallerySettings settings, ILightbox lightbox, bool getAll, DateTime now)
        {
            var images = DataCache.GetCachedData<IEnumerable<ImageViewModel>> (
                new CacheItemArgs ($"//r7_MiniGallery?TabModuleId={moduleContext.TabModuleId}", 1200),
                (c) => GetImages (moduleContext, settings, lightbox)
            );

            var comparer = new ImageComparer (settings.SortAscending);
            return images
                .Where (img => img.IsPublished (now) || moduleContext.IsEditable)
                .OrderBy (img => img, comparer)
                .Take ((!getAll && settings.NumberOfRecords > 0) ? settings.NumberOfRecords : int.MaxValue)
                .ToList ();
        }

        protected IEnumerable<ImageViewModel> GetImages (ModuleInstanceContext moduleContext,
                                                         MiniGallerySettings settings,
                                                         ILightbox lightbox)
        {
            return new MiniGalleryDataProvider ().GetObjects<ImageInfo> (moduleContext.ModuleId)
                                                 .Select (img => new ImageViewModel (img, moduleContext, settings, lightbox));
        }
    }
}
