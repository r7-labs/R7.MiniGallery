using System;

using DotNetNuke.Data;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.FileSystem;


namespace R7.MiniGallery
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
	public class ImageInfo : EntityBase
	{

		#region Fields


		#endregion

		/// <summary>
		/// Empty default cstor
		/// </summary>
		public ImageInfo ()
		{
		}

		#region Properties

		public int ImageID { get; set; }

		public int ModuleID { get; set; }

		public int ImageFileID { get; set; }

		public string Alt { get; set; }

		public string Title { get; set; }

		public string Url { get; set; }

		public int SortIndex { get; set; }

		public bool IsPublished { get; set; }

		#endregion

		#region Joins

		private FileInfo thumbFile;

		[IgnoreColumn]
		public FileInfo ThumbFile
		{
			get
			{
				if (thumbFile == null)
					thumbFile = (FileInfo)FileManager.Instance.GetFile (ImageFileID);
				return thumbFile;
			}
		}

		private ModuleInfo module;

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

		private string createdByUserName;

		[IgnoreColumn]
		public string CreatedByUserName
		{ 
			get
			{
				if (createdByUserName == null)
				{
					var portalId = PortalController.GetCurrentPortalSettings ().PortalId;
					var user = UserController.GetUserById (portalId, CreatedByUserID);
					if (user != null)
						createdByUserName = user.DisplayName;
					else
						createdByUserName = "unknown";
				}
				return createdByUserName;
			}
		}

		private string lastModifiedByUserName;

		[IgnoreColumn]
		public string LastModifiedByUserName
		{ 
			get
			{
				if (lastModifiedByUserName == null)
				{
					var portalId = PortalController.GetCurrentPortalSettings ().PortalId;
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
			thumbFile = null;
			module = null;
			createdByUserName = null;
			lastModifiedByUserName = null;
		}

		#endregion
	}
}

