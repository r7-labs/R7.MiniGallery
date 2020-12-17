using System;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.UI.Modules;
using Newtonsoft.Json.Linq;
using R7.Dnn.Extensions.Text;
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

        public string Alt => GetAlt ();

        public int CreatedByUserID => Model.CreatedByUserID;

        public DateTime CreatedOnDate => Model.CreatedOnDate;

        public int LastModifiedByUserID => Model.LastModifiedByUserID;

        public DateTime LastModifiedOnDate => Model.LastModifiedOnDate;

        public int ImageID => Model.ImageID;

        public int ImageFileID => Model.ImageFileID;

        public DateTime? StartDate => Model.StartDate;

        public DateTime? EndDate => Model.EndDate;

        public int ModuleID => Model.ModuleID;

        public int SortIndex => Model.SortIndex;

        public string Title => Model.Title;

        public string Url => Model.Url;

        public bool OpenInLightbox => Model.OpenInLightbox;

        public string CssClass {
            get {
                return FormatHelper.JoinNotNullOrEmpty (
                    " ", !string.IsNullOrEmpty (Model.CssClass)? Model.CssClass : Settings.ImageCssClass,
                    (!Model.IsPublished (HttpContext.Current.Timestamp)) ?
                        (Model.HasBeenExpired (HttpContext.Current.Timestamp) ?
                            "r7-mg-not-published r7-mg-expired" : "r7-mg-not-published") : string.Empty
                );
            }
        }

        #endregion

        public bool IsHidden { get; set; }

        public string ImageSrc => Globals.LinkClick ("FileID=" + Model.ImageFileID, ModuleContext.TabId, ModuleContext.ModuleId, false);

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

                    if (Settings.GetThumbWidth () > 0)
                        hanglerUrl += "&width={width}";

                    if (Settings.GetThumbHeight () > 0)
                        hanglerUrl += "&height={height}";
                }

                foreach (var tag in imageHandlerTags) {
                    var enclosedTag = "{" + tag + "}";

                    switch (tag) {
                    case "fileticket":
                        hanglerUrl = hanglerUrl.Replace (enclosedTag, UrlUtils.EncryptParameter (Model.ImageFileID.ToString ()));
                        break;

                    case "width":
                        hanglerUrl = hanglerUrl.Replace (enclosedTag, Settings.GetThumbWidth ().ToString ());
                        break;

                    case "fileid":
                        hanglerUrl = hanglerUrl.Replace (enclosedTag, Model.ImageFileID.ToString ());
                        break;

                    case "height":
                        hanglerUrl = hanglerUrl.Replace (enclosedTag, Settings.GetThumbHeight ().ToString ());
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
                if (!string.IsNullOrWhiteSpace(Model.Url)) {
                    var urlType = Globals.GetURLType(Model.Url);
                    if (urlType == TabType.File || urlType == TabType.Tab || urlType == TabType.Member) {
                        return Globals.LinkClick (Model.Url, ModuleContext.TabId, ModuleContext.ModuleId, true);
                    }
                    return Model.Url;
                }
                return ImageSrc;
            }
        }

        public string EditUrl => ModuleContext.EditUrl ("imageid", Model.ImageID.ToString (), "Edit");

        public ImageStyle Style {
            get {
                var style = new ImageStyle ();
                if (Settings.ImageWidth.IsEmpty && Settings.ImageHeight.IsEmpty) {
                    if (Settings.GetThumbWidth () > 0 && Settings.GetThumbHeight () > 0) {
                        // If both ThumbWidth & ThumbHeight are not null, produced image dimensions are determined
                        // also by ResizeMode image handler param. Default is "Fit" - so, by example, if produced
                        // images have same width, height may vary, and vice versa.
                    } else if (Settings.GetThumbWidth () > 0) {
                        style.Width = Unit.Pixel (Settings.GetThumbWidth ()).ToString ();
                    } else if (Settings.ThumbHeight > 0) {
                        style.Height = Unit.Pixel (Settings.GetThumbHeight ()).ToString ();
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
                var lightbox = OpenInLightbox ? Lightbox : LightboxFactory.Create (LightboxType.None);
                return new JRaw (lightbox.GetLinkAttributes (Model, ModuleContext.ModuleId));
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
