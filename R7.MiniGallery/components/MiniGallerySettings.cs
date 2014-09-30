//
// MiniGallerySettings.cs
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
		/// <summary>
		/// Initializes a new instance of the <see cref="R7.MiniGallery.MiniGallerySettings"/> class.
		/// </summary>
		/// <param name="module">Module.</param>
		public MiniGallerySettings (IModuleControl module) : base (module)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="R7.MiniGallery.MiniGallerySettings"/> class.
		/// </summary>
		/// <param name="module">Module.</param>
		public MiniGallerySettings (ModuleInfo module) : base (module)
		{
		}

		#region Module settings

		// NOTE: All current MiniGallery module settings are presentation related

		#endregion
		
		#region TabModule settings
		
		/// <summary>
		/// Gets or sets the type of the lightbox.
		/// </summary>
		/// <value>The type of the lightbox.</value>
		public LightboxType LightboxType
		{
			get { return ReadSetting<LightboxType> ("MiniGallery_LightboxType", LightboxType.LightBox); }
			set { WriteTabModuleSetting<LightboxType> ("MiniGallery_LightboxType", value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="R7.MiniGallery.MiniGallerySettings"/> use scrollbar.
		/// </summary>
		/// <value><c>true</c> if use scrollbar; otherwise, <c>false</c>.</value>
		public bool UseScrollbar
		{
			get { return ReadSetting<bool> ("MiniGallery_UseScrollbar", true); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_UseScrollbar", value); }
		}

		/// <summary>
		/// Gets or sets the style set.
		/// </summary>
		/// <value>The style set.</value>
		public string StyleSet
		{
			get { return ReadSetting<string> ("MiniGallery_StyleSet", "Default"); }
			set { WriteTabModuleSetting<string> ("MiniGallery_StyleSet", value); }
		}

		/// <summary>
		/// Gets or sets the maximum height of a module
		/// </summary>
		/// <value>Gets or sets the maximum height of a module</value>
		public Unit MaxHeight
		{
			get { return ReadSetting<Unit> ("MiniGallery_MaxHeight", Unit.Empty); }
			set { WriteTabModuleSetting<Unit> ("MiniGallery_MaxHeight", value); }
		}

		/// <summary>
		/// Gets or sets the target.
		/// </summary>
		/// <value>The target.</value>
		public string Target
		{
			get { return ReadSetting<string> ("MiniGallery_Target", "_blank"); }
			set { WriteTabModuleSetting<string> ("MiniGallery_Target", value); }
		}

		/// <summary>
		/// Gets or sets the width of the image.
		/// </summary>
		/// <value>The width of the image.</value>
		public Unit ImageWidth
		{
			get { return ReadSetting<Unit> ("MiniGallery_ImageWidth", Unit.Empty); }
			set { WriteTabModuleSetting<Unit> ("MiniGallery_ImageWidth", value); }
		}

		/// <summary>
		/// Gets or sets the height of the image.
		/// </summary>
		/// <value>The height of the image.</value>
		public Unit ImageHeight
		{
			get { return ReadSetting<Unit> ("MiniGallery_ImageHeight", Unit.Empty); }
			set { WriteTabModuleSetting<Unit> ("MiniGallery_ImageHeight", value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="R7.MiniGallery.MiniGallerySettings"/> use image handler.
		/// </summary>
		/// <value><c>true</c> if use image handler; otherwise, <c>false</c>.</value>
		public bool UseImageHandler 
		{
			get { return ReadSetting<bool> ("MiniGallery_UseImageHandler", true); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_UseImageHandler", value); }
		}
		
		/// <summary>
		/// Gets or sets the image handler parameters.
		/// </summary>
		/// <value>The image handler parameters.</value>
		public string ImageHandlerParams
		{
			// HACK: DNN SPs have a special handle of SettingValues starting with "fileid" 
			get { return ReadSetting<string> ("MiniGallery_ImageHandlerParams", "__").TrimStart('_'); }
			set { WriteTabModuleSetting<string> ("MiniGallery_ImageHandlerParams", "__" + value ); }
		}
		
		/// <summary>
		/// Gets or sets the width of the thumb.
		/// </summary>
		/// <value>The width of the thumb.</value>
		public int ThumbWidth
		{
			get { return ReadSetting<int> ("MiniGallery_ThumbWidth", 240); }
			set { WriteTabModuleSetting<int> ("MiniGallery_ThumbWidth", value); }
		}
		
		/// <summary>
		/// Gets or sets the height of the thumb.
		/// </summary>
		/// <value>The height of the thumb.</value>
		public int ThumbHeight
		{
			get { return ReadSetting<int> ("MiniGallery_ThumbHeight", Null.NullInteger); }
			set { WriteTabModuleSetting<int> ("MiniGallery_ThumbHeight", value); }
		}		

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="R7.MiniGallery.MiniGallerySettings"/> show info.
		/// </summary>
		/// <value><c>true</c> if show info; otherwise, <c>false</c>.</value>
		public bool ShowInfo
		{
			get { return ReadSetting<bool> ("MiniGallery_ShowInfo", false); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_ShowInfo", value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="R7.MiniGallery.MiniGallerySettings"/> show titles.
		/// </summary>
		/// <value><c>true</c> if show titles; otherwise, <c>false</c>.</value>
		public bool ShowTitles
		{
			get { return ReadSetting<bool> ("MiniGallery_ShowTitles", true); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_ShowTitles", value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="R7.MiniGallery.MiniGallerySettings"/> expand columns.
		/// </summary>
		/// <value><c>true</c> if expand columns; otherwise, <c>false</c>.</value>
		public bool ExpandColumns
		{
			get { return ReadSetting<bool> ("MiniGallery_ExpandColumns", false); }
			set { WriteTabModuleSetting<bool> ("MiniGallery_ExpandColumns", value); }
		}

		/// <summary>
		/// Gets or sets the columns.
		/// </summary>
		/// <value>The columns.</value>
		public int Columns
		{
			get { return ReadSetting<int> ("MiniGallery_Columns", Null.NullInteger); }
			set { WriteTabModuleSetting<int> ("MiniGallery_Columns", value); }
		}
		
		/// <summary>
		/// Gets or sets the number of records.
		/// </summary>
		/// <value>The number of records.</value>
		public int NumberOfRecords
		{
			get { return ReadSetting<int> ("MiniGallery_NumberOfRecords", Null.NullInteger); }
			set { WriteTabModuleSetting<int> ("MiniGallery_NumberOfRecords", value); }
		}

		/// <summary>
		/// Gets or sets the sort order.
		/// </summary>
		/// <value>The sort order.</value>
		public string SortOrder
		{
			get { return ReadSetting<string> ("MiniGallery_SortOrder", "SortIndex"); }
			set { WriteTabModuleSetting<string> ("MiniGallery_SortOrder", value); }
		}
		
        #endregion
	}
}

