//
//  MiniGallerySettings.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014-2017 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules.Settings;
using R7.MiniGallery.Lightboxes;

namespace R7.MiniGallery.Models
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
        /// Gets or sets the style set.
        /// </summary>
        /// <value>The style set.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public string StyleSet { get; set; } = "Default";

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
        public int ThumbWidth { get; set; } = 262;

        /// <summary>
        /// Gets or sets the height of the thumb.
        /// </summary>
        /// <value>The height of the thumb.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public int ThumbHeight { get; set; } = 0;

        /// <summary>
        /// Gets or sets a value indicating whether module shows image titles.
        /// </summary>
        /// <value><c>true</c> if show titles; otherwise, <c>false</c>.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public bool ShowTitles { get; set; } = true;

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>The columns.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public int Columns { get; set; } = 0; // Auto

        /// <summary>
        /// Gets or sets the number of records.
        /// </summary>
        /// <value>The number of records.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public int NumberOfRecords { get; set; } = 0;
           
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public bool DisableMoreImages { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        [TabModuleSetting (Prefix = "MiniGallery_")]
        public string SortOrder { get; set; } = "SortIndex";

        public bool SortAscending {
            get { return SortOrder == "SortIndex"; }
            set { SortOrder = value ? "SortIndex" : "-SortIndex"; }
        }

        [TabModuleSetting (Prefix = "MiniGallery_")]
        public string ImageCssClass { get; set; }
    }
}

