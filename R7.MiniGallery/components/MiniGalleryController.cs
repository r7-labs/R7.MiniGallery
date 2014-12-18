//
// MiniGalleryController.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014 
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
using System.Text;
using System.Xml;
using System.Linq;
using DotNetNuke.Collections;
using DotNetNuke.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using DotNetNuke.Services.Search.Entities;

namespace R7.MiniGallery
{
	public partial class MiniGalleryController : ControllerBase  /* : IPortable */
	{
		#region Public methods

		/// <summary>
		/// Initializes a new instance of the <see cref="MiniGallery.MiniGalleryController"/> class.
		/// </summary>
		public MiniGalleryController () : base ()
		{ 

		}

		#endregion

        #region ModuleSearchBase implementaion

        public override IList<SearchDocument> GetModifiedSearchDocuments (ModuleInfo modInfo, DateTime beginDate)
        {
            var searchDocs = new List<SearchDocument> ();
            var settings = new MiniGallerySettings (modInfo);

            var images = GetImagesTopN (modInfo.ModuleID, false, 
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

		#region Class-specific controller members (example)

		/*
		public IList<ImageInfo> GetImages (int moduleId, bool showAll, bool sortAscending)
		{
			var sortOrder = sortAscending ? "ASC" : "DESC";

			if (showAll)
				return GetList<ImageInfo> ("WHERE ModuleId = @0 ORDER BY SortIndex " + sortOrder, moduleId);

			return GetList<ImageInfo> ("WHERE ModuleId = @0 AND IsPublished=1 ORDER BY SortIndex " + sortOrder, moduleId);
		}
		*/

		public IEnumerable<ImageInfo> GetImagesTopN (int moduleId, bool showAll, bool sortAscending, int topn = -1)
		{
			var sql = string.Format (
				          "SELECT {0} * FROM dbo.MiniGallery_Images WHERE ModuleId={1} {2} ORDER BY SortIndex {3};",
				          !Null.IsNull(topn) ? string.Format ("TOP({0})", topn) : string.Empty, 
				          moduleId,
				          !showAll ? "AND IsPublished=1" : string.Empty,
				          sortAscending ? "ASC" : "DESC"
			          );
		
			return GetObjects<ImageInfo> (System.Data.CommandType.Text, sql); 
		}

		#endregion

		/*

        #region IPortable members

		/// <summary>
		/// Exports a module to XML
		/// </summary>
		/// <param name="ModuleID">a module ID</param>
		/// <returns>XML string with module representation</returns>
		public string ExportModule (int moduleId)
		{
			var sb = new StringBuilder ();
			var infos = GetObjects<MiniGalleryInfo> (moduleId);

			if (infos != null) {
				sb.Append ("<MiniGallerys>");
				foreach (var info in infos) {
					sb.Append ("<MiniGallery>");
					sb.Append ("<content>");
					sb.Append (XmlUtils.XMLEncode (info.Content));
					sb.Append ("</content>");
					sb.Append ("</MiniGallery>");
				}
				sb.Append ("</MiniGallerys>");
			}
			
			return sb.ToString ();
		}

		/// <summary>
		/// Imports a module from an XML
		/// </summary>
		/// <param name="ModuleID"></param>
		/// <param name="Content"></param>
		/// <param name="Version"></param>
		/// <param name="UserID"></param>
		public void ImportModule (int ModuleID, string Content, string Version, int UserID)
		{
			var infos = DotNetNuke.Common.Globals.GetContent (Content, "MiniGallerys");
		
			foreach (XmlNode info in infos.SelectNodes("MiniGallery")) {
				var item = new MiniGalleryInfo ();
				item.ModuleID = ModuleID;
				item.Content = info.SelectSingleNode ("content").InnerText;
				item.CreatedByUser = UserID;

				Add<MiniGalleryInfo> (item);
			}
		}
		
	    #endregion
*/
	}
}

