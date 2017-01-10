//
// MiniGallerySettings.cs
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
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules.Settings;

namespace R7.MiniGallery
{
    [Serializable]
    public class MiniGallerySettings
    {
        /// <summary>
        /// Gets or sets the type of the lightbox.
        /// </summary>
        /// <value>The type of the lightbox.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public LightboxType LightboxType { get; set; } = LightboxType.Default;

        /// <summary>
        /// Gets or sets a value indicating whether the module use scrollbar.
        /// </summary>
        /// <value><c>true</c> if use scrollbar; otherwise, <c>false</c>.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public bool UseScrollbar { get; set; } = true;

        /// <summary>
        /// Gets or sets the style set.
        /// </summary>
        /// <value>The style set.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public string StyleSet { get; set; } = "Fixed";

        /// <summary>
        /// Gets or sets the maximum height of a module
        /// </summary>
        /// <value>Gets or sets the maximum height of a module</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public Unit MaxHeight { get; set; } = Unit.Empty;

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public string Target { get; set; } = "_blank";

        /// <summary>
        /// Gets or sets the width of the image.
        /// </summary>
        /// <value>The width of the image.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public Unit ImageWidth { get; set; } = Unit.Empty;

        /// <summary>
        /// Gets or sets the height of the image.
        /// </summary>
        /// <value>The height of the image.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public Unit ImageHeight { get; set; } = Unit.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the module should use image handler.
        /// </summary>
        /// <value><c>true</c> if use image handler; otherwise, <c>false</c>.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public bool UseImageHandler { get; set; } = true;

        /// <summary>
        /// Gets or sets the image handler parameters.
        /// </summary>
        /// <value>The image handler parameters.</value>
        [TabModuleSetting (ParameterName = "ImageHandlerParams", Prefix = "MiniGallery_")]
        public string ImageHandlerParams_Internal { get; set; }

        // HACK: DNN SPs have a special handle of SettingValues starting with "fileid" 
        public string ImageHandlerParams {
            get { return ImageHandlerParams_Internal?.TrimStart ('_'); }
            set { ImageHandlerParams_Internal = "__" + value; }
        }

        /// <summary>
        /// Gets or sets the width of the thumb.
        /// </summary>
        /// <value>The width of the thumb.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public int ThumbWidth { get; set; } = 240;

        /// <summary>
        /// Gets or sets the height of the thumb.
        /// </summary>
        /// <value>The height of the thumb.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public int ThumbHeight { get; set; } = Null.NullInteger;

        /// <summary>
        /// Gets or sets a value indicating whether module show image info.
        /// </summary>
        /// <value><c>true</c> if show info; otherwise, <c>false</c>.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public bool ShowInfo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether module shows image titles.
        /// </summary>
        /// <value><c>true</c> if show titles; otherwise, <c>false</c>.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public bool ShowTitles { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the module expand columns.
        /// </summary>
        /// <value><c>true</c> if expand columns; otherwise, <c>false</c>.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public bool ExpandColumns { get; set; } = false;

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>The columns.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public int Columns { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of records.
        /// </summary>
        /// <value>The number of records.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public int NumberOfRecords { get; set; } = Null.NullInteger;

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public string SortOrder { get; set; } = "SortIndex";
	}
}

