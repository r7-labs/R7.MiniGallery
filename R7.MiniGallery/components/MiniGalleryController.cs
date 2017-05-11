//
//  MiniGalleryController.cs
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

using System;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search.Entities;
using R7.DotNetNuke.Extensions.Utilities;
using R7.MiniGallery.Data;
using R7.MiniGallery.Models;

namespace R7.MiniGallery
{
    public class MiniGalleryController : ModuleSearchBase
	{
		#region ModuleSearchBase implementaion

        public override IList<SearchDocument> GetModifiedSearchDocuments (ModuleInfo modInfo, DateTime beginDate)
        {
            var searchDocs = new List<SearchDocument> ();
            var settings = new MiniGallerySettingsRepository ().GetSettings (modInfo);
            var now = DateTime.Now;

            var images = new MiniGalleryDataProvider ().GetImagesTopN (modInfo.ModuleID, false, 
                settings.SortOrder == "SortIndex", settings.NumberOfRecords);

            foreach (var image in images ?? Enumerable.Empty<ImageInfo>())
            {
                if (image.LastModifiedOnDate.ToUniversalTime () > beginDate.ToUniversalTime ())
                {
                    var imageTitle = !string.IsNullOrWhiteSpace (image.Title) ? image.Title : image.Alt;

                    // add only images with text
                    if (!string.IsNullOrWhiteSpace (imageTitle))
                    {
                        var sd = new SearchDocument ()
                        {
                            PortalId = modInfo.PortalID,
                            AuthorUserId = image.LastModifiedByUserID,
                            Title = imageTitle,
                            Body = TextUtils.FormatList (" ", image.Alt, image.Title),
                            ModifiedTimeUtc = image.LastModifiedOnDate.ToUniversalTime (),
                            UniqueKey = string.Format ("MiniGallery_Image_{0}", image.ImageID),
                            Url = string.Format ("/Default.aspx?tabid={0}#{1}", modInfo.TabID, modInfo.ModuleID),
                            IsActive = image.IsPublished (now)
                        };

                        searchDocs.Add (sd);
                    }
                }

            }

            return searchDocs;
        }

        #endregion
	}
}
