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
			get { return ReadSetting<bool> ("MiniGallery_UseImageHandler", true, false); }
			set { WriteSetting<bool> ("MiniGallery_UseImageHandler", value, false); }
		}

		public string ImageHandlerFormat
		{
			get { return ReadSetting<string> ("MiniGallery_ImageHandlerFormat", "width={width}", false); }
			set { WriteSetting<string> ("MiniGallery_ImageHandlerFormat", value, false); }
		}

		#endregion
		
		#region TabModule settings

		/// <summary>
		/// A jQuery viewer, used by gallery. Default is YoxView
		/// </summary>
		/// <value>A jQuery viewer, used by gallery</value>
		public bool UseViewer
		{
			get { return ReadSetting<bool> ("MiniGallery_UseViewer", true, true); }
			set { WriteSetting<bool> ("MiniGallery_UseViewer", value, true); }
		}

		public bool UseScrollbar
		{
			get { return ReadSetting<bool> ("MiniGallery_UseScrollbar", true, true); }
			set { WriteSetting<bool> ("MiniGallery_UseScrollbar", value, true); }
		}

		public string StyleSet
		{
			get { return ReadSetting<string> ("MiniGallery_StyleSet", "Default", true); }
			set { WriteSetting<string> ("MiniGallery_StyleSet", value, true); }
		}

		/*
		public string ImageCssClass
		{
			get { return ReadSetting<string> ("MiniGallery_ImageCssClass", "MiniGallery_Image", true); }
			set { WriteSetting ("MiniGallery_ImageCssClass", value, true); }
		}

		public string ImageListCssClass
		{
			get { return ReadSetting<string> ("MiniGallery_ImageListCssClass", "MiniGallery_ImageList", true); }
			set { WriteSetting ("MiniGallery_ImageListCssClass", value, true); }
		}
		*/

		/// <summary>
		/// Gets or sets the maximum height of a module
		/// </summary>
		/// <value>Gets or sets the maximum height of a module</value>
		public int MaxHeight
		{
			get { return ReadSetting<int> ("MiniGallery_MaxHeight", Null.NullInteger, true); }
			set { WriteSetting<int> ("MiniGallery_MaxHeight", value, true); }
		}

		public string Target
		{
			get { return ReadSetting<string> ("MiniGallery_Target", "_blank", true); }
			set { WriteSetting<string> ("MiniGallery_Target", value, true); }
		}

		public Unit FrameWidth
		{
			get { return Unit.Parse(ReadSetting<string> ("MiniGallery_FrameWidth", string.Empty, true)); }
			set { WriteSetting<string> ("MiniGallery_FrameWidth", value.ToString(), true); }
		}

		public Unit FrameHeight
		{
			get { return Unit.Parse(ReadSetting<string> ("MiniGallery_FrameHeight", string.Empty, true)); }
			set { WriteSetting<string> ("MiniGallery_FrameHeight", value.ToString(), true); }
		}

		public int? ImageWidth
		{
			get 
			{ 
				var imageWidth = ReadSetting<int> ("MiniGallery_ImageWidth", 240, true);
				return Null.IsNull (imageWidth) ? null : (int?)imageWidth; 
			}
			set { WriteSetting<int> ("MiniGallery_ImageWidth", value ?? Null.NullInteger, true); }
		}
		
		public int? ImageHeight
		{
			get 
			{ 
				var imageHeight = ReadSetting<int> ("MiniGallery_ImageHeight", Null.NullInteger, true);
				return Null.IsNull(imageHeight)? null : (int?)imageHeight;
			}
			set { WriteSetting<int> ("MiniGallery_ImageHeight", value ?? Null.NullInteger, true); }
		}		

		public bool ShowInfo
		{
			get { return ReadSetting<bool> ("MiniGallery_ShowInfo", false, true); }
			set { WriteSetting<bool> ("MiniGallery_ShowInfo", value, true); }
		}

		public bool ShowTitles
		{
			get { return ReadSetting<bool> ("MiniGallery_ShowTitles", true, true); }
			set { WriteSetting<bool> ("MiniGallery_ShowTitles", value, true); }
		}

		public bool ExpandColumns
		{
			get { return ReadSetting<bool> ("MiniGallery_ExpandColumns", false, true); }
			set { WriteSetting<bool> ("MiniGallery_ExpandColumns", value, true); }
		}

		public int Columns
		{
			get { return ReadSetting<int> ("MiniGallery_Columns", Null.NullInteger, true); }
			set { WriteSetting<int> ("MiniGallery_Columns", value, true); }
		}
		
		public int NumberOfRecords
		{
			get { return ReadSetting<int> ("MiniGallery_NumberOfRecords", Null.NullInteger, true); }
			set { WriteSetting<int> ("MiniGallery_NumberOfRecords", value, true); }
		}

		/*
		public int Rows
		{
			get { return ReadSetting<int> ("MiniGallery_Rows", Null.NullInteger, true); }
			set { WriteSetting<int> ("MiniGallery_Rows", value, true); }
		}*/
		
        #endregion
	}
}

