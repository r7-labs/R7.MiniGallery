using System;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace R7.MiniGallery.Models
{
	// More attributes for class:
	// Set caching for table: [Cacheable("R7.MiniGallery_Images", CacheItemPriority.Default, 20)]
	// Explicit mapping declaration: [DeclareColumns]
    // More attributes for class properties:
	// Custom column name: [ColumnName("ImageID")]
	// Explicit include column: [IncludeColumn]
	// Note: DAL 2 have no AutoJoin analogs from PetaPOCO at this time

	[TableName ("MiniGallery_Images")]
	[PrimaryKey ("ImageID", AutoIncrement = true)]
	[Scope ("ModuleID")]
	public class ImageInfo : IImageWritable
	{
        #region IImage implementation

		public int ImageID { get; set; }

		public int ModuleID { get; set; }

		public int ImageFileID { get; set; }

		public string Alt { get; set; }

		public string Title { get; set; }

		public string Url { get; set; }

        public bool OpenInLightbox { get; set; }

        public string CssClass { get; set; }

        public int SortIndex { get; set; }

		public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int LastModifiedByUserID { get; set; }

        public DateTime LastModifiedOnDate { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedOnDate { get; set; }

        #endregion

        #region Joins

        private string createdByUserName;

        // TODO: Move to ViewModel
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

        private string lastModifiedByUserName;

        // TODO: Move to ViewModel
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

		#endregion
	}
}

