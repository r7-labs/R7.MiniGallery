//
// ImageInfo.cs
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
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.FileSystem;

namespace R7.MiniGallery.Models
{
	// More attributes for class:
	// Set caching for table: [Cacheable("Redhound.MiniGallery_Images", CacheItemPriority.Default, 20)]
	// Explicit mapping declaration: [DeclareColumns]
	
	// More attributes for class properties:
	// Custom column name: [ColumnName("ImageID")]
	// Explicit include column: [IncludeColumn]
	// Note: DAL 2 have no AutoJoin analogs from PetaPOCO at this time
	[TableName ("MiniGallery_Images")]
	[PrimaryKey ("ImageID", AutoIncrement = true)]
	[Scope ("ModuleID")]
	public class ImageInfo : IImage
	{
		#region Fields
		
		private ModuleInfo module;

		private IFileInfo file;

		private string createdByUserName;

		private string lastModifiedByUserName;

		#endregion

		#region IImage implementation

		public int ImageID { get; set; }

		public int ModuleID { get; set; }

		public int ImageFileID { get; set; }

		public string Alt { get; set; }

		public string Title { get; set; }

		public string Url { get; set; }

		public int SortIndex { get; set; }

		public bool IsPublished { get; set; }

		#endregion

        #region IAuditable implementation

        public int LastModifiedByUserID { get; set; }

        public DateTime LastModifiedOnDate { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedOnDate { get; set; }

        #endregion

        #region Joins

        [IgnoreColumn]
		public IFileInfo File
		{
			get
			{
				if (file == null)
					file = FileManager.Instance.GetFile (ImageFileID);
				
				return file;
			}
		}

		[IgnoreColumn]
		public ModuleInfo Module
		{
			get
			{
				if (module == null)
				{
					var mc = new ModuleController ();
					module = mc.GetModule (ModuleID);
				}
				return module;
			}
		}

		[IgnoreColumn]
		public string CreatedByUserName
		{ 
			get
			{
				if (createdByUserName == null)
				{
                    var portalId = PortalController.Instance.GetCurrentPortalSettings ().PortalId;
					var user = UserController.GetUserById (portalId, CreatedByUserID);
					if (user != null)
						createdByUserName = user.DisplayName;
					else
						createdByUserName = "unknown";
				}
				return createdByUserName;
			}
		}

		[IgnoreColumn]
		public string LastModifiedByUserName
		{ 
			get
			{
				if (lastModifiedByUserName == null)
				{
                    var portalId = PortalController.Instance.GetCurrentPortalSettings ().PortalId;
					var user = UserController.GetUserById (portalId, LastModifiedByUserID);
					if (user != null)
						lastModifiedByUserName = user.DisplayName;
					else
						lastModifiedByUserName = "unknown";
				}
				return lastModifiedByUserName;
			}
		}

		public void ResetJoins ()
		{
			file = null;
			module = null;
			createdByUserName = null;
			lastModifiedByUserName = null;
		}

		#endregion
	}
}

