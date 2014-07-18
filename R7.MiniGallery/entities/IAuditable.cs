using System;

namespace R7.MiniGallery
{
	public interface IAuditable
	{
		int LastModifiedByUserID { get; set; }
		DateTime LastModifiedOnDate { get; set; }
		int CreatedByUserID { get; set; }
		DateTime CreatedOnDate { get; set; }
	}
}
