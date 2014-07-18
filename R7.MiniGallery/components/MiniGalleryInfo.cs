using System;
using System.Linq;

using DotNetNuke.Data;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace R7.MiniGallery
{
	// More attributes for class:
	// Set caching for table: [Cacheable("MiniGallery_MiniGalleryInfos", CacheItemPriority.Default, 20)] 
	// Explicit mapping declaration: [DeclareColumns]
	
	// More attributes for class properties:
	// Custom column name: [ColumnName("MiniGalleryID")]
	// Explicit include column: [IncludeColumn]
	// Note: DAL 2 have no AutoJoin analogs from PetaPOCO at this time
	[TableName("MiniGallery_MiniGallerys")]
	[PrimaryKey("MiniGalleryID", AutoIncrement = true)]
	[Scope("ModuleID")]
	public class MiniGalleryInfo
	{
        #region Fields
        
		private string createdByUserName = null;

		#endregion
		
		/// <summary>
		/// Empty default cstor
		/// </summary>
		public MiniGalleryInfo ()
		{
		}

        #region Properties

		public int MiniGalleryID { get; set; }

		public int ModuleID { get; set; }

		public string Content { get; set; }

		public int CreatedByUser { get; set; }

		[ReadOnlyColumn]
		public DateTime CreatedOnDate { get; set; }
        
		[IgnoreColumn]
		public string CreatedByUserName {
			get {
				if (createdByUserName == null) {
					var portalId = PortalController.GetCurrentPortalSettings ().PortalId;
					var user = UserController.GetUserById (portalId, CreatedByUser);
					createdByUserName = user.DisplayName;
				}

				return createdByUserName; 
			}
		}

        #endregion
        
		/* // Joins example
     	
     	// foreign key
     	public int AnotherID { get; set; }
     	
     	// private object reference
     	private AnotherInfo _another;
     	
     	// public object reference
     	public AnotherInfo Another 
     	{
     	   	// this getter method hide underlying access to database, 
     	   	// and perform simple caching by storing reference
     	   	// to retrived AnotherInfo object in a private field "_another"
     		get 
     		{
     			if (_other == null)
     			{
     				// load joined object to reference it
     				var ctrl = new MiniGalleryController();
     				_another = ctrl.Get<AnotherInfo>(AnotherID);
     			}
     			return _another;	
     		}
     		set 
     		{
     			_another = value;
     		}
     	}      
     	
     	/// <summary>
     	/// Nullifies all private fields with references to joined objects,
     	/// so next access to corresponding object properties 
     	/// results in reloading them from the database  
     	/// </summary>
     	public void ResetJoins ()
     	{
     		_another = null;
     	}
        
        // Now we have ability to use MiniGalleryInfo objects
        // to access members of joined AnotherInfo objects 
        
       	// Get MiniGalleryInfo object by it's primary key (ID):
       	// var ctrl = new MiniGalleryController();
     	// var item = ctrl.Get<MiniGalleryInfo>(itemId);
     	
     	// Now simply get data from another table:
     	// Console.WriteLine(item.Another.SomeProperty);
     	
        // True is, that it is not very effective way to retrieve multiple objects, 
        // but it is 1) simple and 2) object-oriented, so then PetaPOCO AutoJoin 
        // attribute will be included in DAL 2, existing business logic code 
        // can be upgraded with almost no efforts.
       
        */
	}
}

