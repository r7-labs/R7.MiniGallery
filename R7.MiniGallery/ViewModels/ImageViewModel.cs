//
//  ImageViewModel.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
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
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.UI.Modules;
using Newtonsoft.Json.Linq;
using R7.DotNetNuke.Extensions.Utilities;
using R7.MiniGallery.Lightboxes;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.ViewModels
{
    public class ImageViewModel : IImage
    {
        protected static NumberFormatInfo dotDecimalSeparator = new NumberFormatInfo { NumberDecimalSeparator = "." };

        protected IImage Model;

        protected ModuleInstanceContext ModuleContext;

        protected MiniGallerySettings Settings;

        protected ILightbox Lightbox;

        public ImageViewModel (IImage model, ModuleInstanceContext moduleContext, MiniGallerySettings settings, ILightbox lightbox)
        {
            Model = model;
            ModuleContext = moduleContext;
            Settings = settings;
            Lightbox = lightbox;
        }

        #region IImage implementation

        public string Alt {
            get { return GetAlt (); }
            set { throw new NotImplementedException (); }
        }

        public int CreatedByUserID {
            get { return Model.CreatedByUserID; }
            set { throw new NotImplementedException (); }
        }

        public DateTime CreatedOnDate {
            get { return Model.CreatedOnDate; }
            set { throw new NotImplementedException (); }
        }

        public int LastModifiedByUserID {
            get { return Model.LastModifiedByUserID; }
            set { throw new NotImplementedException (); }
        }

        public DateTime LastModifiedOnDate {
            get { return Model.LastModifiedOnDate; }
            set { throw new NotImplementedException (); }
        }

        public int ImageID {
            get { return Model.ImageID; }
            set { throw new NotImplementedException (); }
        }

        public int ImageFileID {
            get { return Model.ImageFileID; }
            set { throw new NotImplementedException (); }
        }

        public DateTime? StartDate {
            get { return Model.StartDate; }
            set { throw new NotImplementedException (); }
        }

        public DateTime? EndDate {
            get { return Model.EndDate; }
            set { throw new NotImplementedException (); }
        }

        public int ModuleID {
            get { return Model.ModuleID; }
            set { throw new NotImplementedException (); }
        }

        public int SortIndex {
            get { return Model.SortIndex; }
            set { throw new NotImplementedException (); }
        }

        public string Title {
            get { return Model.Title; }
            set { throw new NotImplementedException (); }
        }

        public string Url {
            get { return Model.Url; }
            set { throw new NotImplementedException (); }
        }

        #endregion

        public string ImageSrc {
            get {
                return Globals.LinkClick ("FileID=" + Model.ImageFileID, ModuleContext.TabId, ModuleContext.ModuleId, false);
            }
        }

        private string [] imageHandlerTags = { "fileticket", "width", "fileid", "height" };

        public string ThumbnailUrl {
            get {
                if (!Settings.UseImageHandler) {
                    return ImageSrc;
                }
                var hanglerUrl = "/imagehandler.ashx?";

                if (!string.IsNullOrWhiteSpace (Settings.ImageHandlerParams))
                    hanglerUrl += Settings.ImageHandlerParams;
                else {
                    hanglerUrl += "fileticket={fileticket}";

                    if (Settings.ThumbWidth > 0)
                        hanglerUrl += "&width={width}";

                    if (Settings.ThumbHeight > 0)
                        hanglerUrl += "&height={height}";
                }

                foreach (var tag in imageHandlerTags) {
                    var enclosedTag = "{" + tag + "}";

                    switch (tag) {
                    case "fileticket":
                        hanglerUrl = hanglerUrl.Replace (enclosedTag, UrlUtils.EncryptParameter (Model.ImageFileID.ToString ()));
                        break;

                    case "width":
                        hanglerUrl = hanglerUrl.Replace (enclosedTag, Settings.ThumbWidth.ToString ());
                        break;

                    case "fileid":
                        hanglerUrl = hanglerUrl.Replace (enclosedTag, Model.ImageFileID.ToString ());
                        break;

                    case "height":
                        hanglerUrl = hanglerUrl.Replace (enclosedTag, Settings.ThumbHeight.ToString ());
                        break;
                    }
                }

                var file = FileManager.Instance.GetFile (Model.ImageFileID);
                return Globals.AddHTTP (PortalSettings.Current.PortalAlias.HTTPAlias +
                                        hanglerUrl + "&ext=." + file.Extension.ToLowerInvariant ());
            }
        }

        public string NavigateUrl {
            get {
                if (!string.IsNullOrWhiteSpace (Model.Url)) {
                    return Globals.LinkClick (Model.Url, ModuleContext.TabId, ModuleContext.ModuleId, false);
                }
                return ImageSrc;
            }
        }

        public string Target {
            get {
                if (Settings.LightboxType == LightboxType.None) {
                    return Settings.Target;
                }
                return string.Empty;
            }
        }

        public string EditUrl {
            get { return ModuleContext.EditUrl ("imageid", Model.ImageID.ToString (), "Edit"); }
        }

        public string CssClass {
            get {
                return TextUtils.FormatList (
                    " ", Settings.ImageCssClass,
                    (!Model.IsPublished (HttpContext.Current.Timestamp)) ? "MG_NotPublished" : string.Empty
                );
            }
        }

        public ImageStyle Style {
            get {
                var style = new ImageStyle ();
                if (Settings.ImageWidth.IsEmpty && Settings.ImageHeight.IsEmpty) {
                    if (Settings.ThumbWidth > 0 && Settings.ThumbHeight > 0) {
                        // If both ThumbWidth & ThumbHeight are not null, produced image dimensions are determined
                        // also by ResizeMode image handler param. Default is "Fit" - so, by example, if produced
                        // images have same width, height may vary, and vice versa.
                    } else if (Settings.ThumbWidth > 0) {
                        style.Width = Unit.Pixel (Settings.ThumbWidth).ToString ();
                    } else if (Settings.ThumbHeight > 0) {
                        style.Height = Unit.Pixel (Settings.ThumbHeight).ToString ();
                    }
                } else {
                    if (!Settings.ImageWidth.IsEmpty)
                        style.Width = Settings.ImageWidth.ToString ();

                    if (!Settings.ImageHeight.IsEmpty)
                        style.Height = Settings.ImageHeight.ToString ();
                }

                return style;
            }
        }

        public JRaw LinkAttributes {
            get {
                return new JRaw (Lightbox.GetLinkAttributes (Model, ModuleContext.ModuleId));
            }
        }

        public JRaw ItemStyle {
            get {
                if (Settings.Columns > 0) {
                    // WTF: Unit.ToString() uses culture-dependent decimal separator?!
                    var colWidth = Unit.Percentage (Math.Round (100.0 / Settings.Columns, 2)).ToString (dotDecimalSeparator);
                    return new JRaw ($"{{\"width\":\"{colWidth}\"}}");
                }
                return new JRaw ("{}");
            }
        }

        protected string GetAlt ()
        {
            if (!string.IsNullOrEmpty (Model.Alt)) {
                return Model.Alt;
            }

            if (!string.IsNullOrEmpty (Model.Title)) {
                return Model.Title;
            }

            var moduleTitle = ModuleContext.Configuration.ModuleTitle;
            if (!string.IsNullOrEmpty (moduleTitle) && moduleTitle != "R7.MiniGallery") {
                return ModuleContext.Configuration.ModuleTitle;
            }

            var activeTab = PortalSettings.Current.ActiveTab;
            if (!string.IsNullOrEmpty (activeTab.Title)) {
                return activeTab.Title;
            }

            return string.Empty;
        }
    }

    public class ImageStyle
    {
        public string Width { get; set; }

        public string Height { get; set; }
    }
}
