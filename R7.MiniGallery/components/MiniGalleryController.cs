//
// MiniGalleryController.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2017
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search.Entities;
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
                            // Description = HtmlUtils.Shorten (image.Description, 255, "..."),
                            Body = Utils.FormatList (" ", image.Alt, image.Title),
                            ModifiedTimeUtc = image.LastModifiedOnDate.ToUniversalTime (),
                            UniqueKey = string.Format ("MiniGallery_Image_{0}", image.ImageID),
                            Url = string.Format ("/Default.aspx?tabid={0}#{1}", modInfo.TabID, modInfo.ModuleID),
                            IsActive = image.IsPublished
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
