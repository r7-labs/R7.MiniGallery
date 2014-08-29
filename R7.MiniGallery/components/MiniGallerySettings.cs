// TODO: Shorter setting prefix, e.g. MiniGallery_ => r7mg_

using System;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.UI.Modules;

namespace R7.MiniGallery
{
	/// <summary>
	/// Provides strong typed access to settings used by module
	/// </summary>
	public partial class MiniGallerySettings : SettingsWrapper
	{
		public MiniGallerySettings (IModuleControl module) : base (module)
		{
		}

		public MiniGallerySettings (ModuleInfo module) : base (module)
		{
		}

		#region Module settings

		public bool UseImageHandler 
		{
			get { return ReadSetting<bool> ("MiniGallery_UseImageHandler", true); }
			set { WriteModuleSetting<bool> ("MiniGallery_UseImageHandler", value); }
		}
		
		public string ImageHandlerParams
		{
			get { return ReadSetting<string> ("MiniGallery_ImageHandlerParams", "fileid={fileid}&width={width}"); }
			set { WriteModuleSetting<string> ("MiniGallery_ImageHandlerParams", value); }
		}

		#endregion
		
		#region TabModule settings
		
		/// <summary>
		/// A jQuery viewer, used by gallery. Default is YoxView
		/// </summary>
		/// <value>A jQuery viewer, used by gallery</value>
		public bool UseLightbox
		{
			get { return ReadSetting<bool> ("MiniGallery_UseLightbox", true); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_UseLightbox", value); }
		}

		public bool UseScrollbar
		{
			get { return ReadSetting<bool> ("MiniGallery_UseScrollbar", true); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_UseScrollbar", value); }
		}

		public string StyleSet
		{
			get { return ReadSetting<string> ("MiniGallery_StyleSet", "Default"); }
			set { WriteTabModuleSetting<string> ("MiniGallery_StyleSet", value); }
		}

		/// <summary>
		/// Gets or sets the maximum height of a module
		/// </summary>
		/// <value>Gets or sets the maximum height of a module</value>
		public int MaxHeight
		{
			get { return ReadSetting<int> ("MiniGallery_MaxHeight", Null.NullInteger); }
			set { WriteTabModuleSetting<int> ("MiniGallery_MaxHeight", value); }
		}

		public string Target
		{
			get { return ReadSetting<string> ("MiniGallery_Target", "_blank"); }
			set { WriteTabModuleSetting<string> ("MiniGallery_Target", value); }
		}

		public Unit ImageWidth
		{
			get { return Unit.Parse(ReadSetting<string> ("MiniGallery_ImageWidth", string.Empty)); }
			set { WriteTabModuleSetting<string> ("MiniGallery_ImageWidth", value.ToString()); }
		}

		public Unit ImageHeight
		{
			get { return Unit.Parse(ReadSetting<string> ("MiniGallery_ImageHeight", string.Empty)); }
			set { WriteTabModuleSetting<string> ("MiniGallery_ImageHeight", value.ToString()); }
		}

		public int ThumbWidth
		{
			get { return ReadSetting<int> ("MiniGallery_ThumbWidth", 240); }
			set { WriteTabModuleSetting<int> ("MiniGallery_ImageWidth", value); }
		}
		
		public int ThumbHeight
		{
			get { return ReadSetting<int> ("MiniGallery_ThumbHeight", Null.NullInteger); }
			set { WriteTabModuleSetting<int> ("MiniGallery_ImageHeight", value); }
		}		

		public bool ShowInfo
		{
			get { return ReadSetting<bool> ("MiniGallery_ShowInfo", false); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_ShowInfo", value); }
		}

		public bool ShowTitles
		{
			get { return ReadSetting<bool> ("MiniGallery_ShowTitles", true); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_ShowTitles", value); }
		}

		public bool ExpandColumns
		{
			get { return ReadSetting<bool> ("MiniGallery_ExpandColumns", false); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_ExpandColumns", value); }
		}

		public int Columns
		{
			get { return ReadSetting<int> ("MiniGallery_Columns", Null.NullInteger); }
			set { WriteTabModuleSetting<int> ("MiniGallery_Columns", value); }
		}
		
		public int NumberOfRecords
		{
			get { return ReadSetting<int> ("MiniGallery_NumberOfRecords", Null.NullInteger); }
			set { WriteTabModuleSetting<int> ("MiniGallery_NumberOfRecords", value); }
		}

		public string SortOrder
		{
			get { return ReadSetting<string> ("MiniGallery_SortOrder", "SortIndex"); }
			set { WriteTabModuleSetting<string> ("MiniGallery_SortOrder", value); }
		}
		
        #endregion
	}
}

