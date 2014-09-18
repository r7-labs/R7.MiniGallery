// TODO: Make styles for vertical and flow layouts
// THINK: How to display headers on images?
// TODO: Simplify styleset names: MG_Default => mgDefault 
// TODO: Use imagehandler only if image dimensions specified in px
// THINK: Don't imagehandler if both width and height isn't set
// THINK: Separate setting for image dimension units (same for width and height)
// THINK: Don't use rows, only columns to get bootstrap-like flow
// THINK: Display image title only on hover

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Icons;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;

namespace R7.MiniGallery
{
	public partial class ViewMiniGallery : MiniGalleryPortalModuleBase, IActionable
	{
		#region Fields
		
		// TODO: Add more R7.Imagehandler tags 

		private string [] imageHandlerTags = new [] { "fileticket", "width", "fileid", "height" };

		protected LightboxBase Lightbox;
		
		#endregion

		#region Properties

		public string EditIconUrl
		{
			get { return IconController.IconURL ("Edit"); }
		}

		#endregion

		#region Handlers

		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);
			
			Lightbox = new Lightbox (Page);
			Lightbox.Register ();
		}

		/// <summary>
		/// Handles Page_Load event for a control
		/// </summary>
		/// <param name="e">Event args.</param>
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);

			try
			{
				if (!IsPostBack)
				{
					// number of columns
					// NOTE: Setting columns to auto need also special styleset!
					if (MiniGallerySettings.Columns > 0)
					{	
						listImages.RepeatColumns = MiniGallerySettings.Columns;
						if (MiniGallerySettings.ExpandColumns)
							listImages.ItemStyle.Width = Unit.Percentage (100 / listImages.RepeatColumns - 1);
					}

					// add current style CSS class to the list
					listImages.CssClass += " MG_" + MiniGallerySettings.StyleSet;

					// if (ImageViewer == ImageViewer.YoxView)
					// 	listImages.CssClass += " yoxview";
							
					// set maximum height of a list
					var maxHeight = MiniGallerySettings.MaxHeight;
					if (maxHeight >= 0)
						listImages.Style.Add ("max-height", maxHeight + "px");

					// get images
					var images = MiniGalleryController.GetImagesTopN (ModuleId, IsEditable, 
						MiniGallerySettings.SortOrder == "SortIndex", MiniGallerySettings.NumberOfRecords);
				
					// check if we have some content to display, 
					// otherwise display a sample default content from the resources
					if (!images.Any ())
					{
						if (IsEditable)
							Utils.Message (this, "AddImages.Help", MessageType.Info, true);
						else 
							// hide module if there are no content to display
							ContainerControl.Visible = false;
					}
					else
					{
						// bind the data
						listImages.DataSource = images;
						listImages.DataBind ();
					}

//					if (listImages.RepeatColumns % 2 == 0 && items.Count % 2 == 1)
//						items.Add(new ImageInfo() {ThumbFileID = Null.NullInteger} );
				}
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		#endregion

		/// <summary>
		/// Handles the items being bound to the datalist control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void listImages_ItemDataBound (object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			// use e.Item.DataItem as object of MiniGalleryInfo class,
			// as we really know it is:
			var image = e.Item.DataItem as ImageInfo;

			// find controls in DataList item template
			// var labelTitle = e.Item.FindControl ("labelTitle") as Label;
			var imageImage = e.Item.FindControl ("imageImage") as Image;
			var linkImage = e.Item.FindControl ("linkImage") as HyperLink;
			var linkEdit = e.Item.FindControl ("linkEdit") as HyperLink;
            
			// fill out the controls

			imageImage.AlternateText = image.Alt;
			imageImage.ToolTip = (!string.IsNullOrWhiteSpace (image.Title))? image.Title : image.Alt;

			#region Link

			// TODO: url type (secured or none) must be set in settings

			if (!string.IsNullOrWhiteSpace (image.Url))
			{	
				linkImage.NavigateUrl = Utils.FormatURL (this, image.Url, false);

				var target = MiniGallerySettings.Target;
				if (target != "none")
					linkImage.Target = target;
			}
			else
			{
				// no url specified, link to image itself
				linkImage.NavigateUrl = Utils.FormatURL (this, "FileID=" + image.ImageFileID, false);
			}

			// lightbox
			if (MiniGallerySettings.UseLightbox)
			{
				Lightbox.ApplyTo (imageImage, linkImage, TabModuleId);
			}

			#endregion

			#region Edit Link

			linkEdit.NavigateUrl = Utils.EditUrl (this, "Edit", "ImageID", image.ImageID.ToString ());

			// without popups support:
			// linkEdit.NavigateUrl = EditUrl (TabId, "Edit",  false, "mid", ModuleId.ToString(), "ImageID", image.ImageID.ToString());

			#endregion

			#region Image

			if (!MiniGallerySettings.UseImageHandler)
			{
				imageImage.ImageUrl = Utils.FormatURL (this, "FileID=" + image.ImageFileID, false);
			}
			else
			{	
				var hanglerUrl = "/imagehandler.ashx?";

				if (!string.IsNullOrWhiteSpace (MiniGallerySettings.ImageHandlerParams))
					hanglerUrl += MiniGallerySettings.ImageHandlerParams;
				else
					hanglerUrl += "fileticket={fileticket}&width={width}";

				foreach (var tag in imageHandlerTags)
				{
					var enclosedTag = "{" + tag + "}";
					
					switch (tag)
					{
						case "fileticket":
							hanglerUrl = hanglerUrl.Replace (enclosedTag, UrlUtils.EncryptParameter (image.ImageFileID.ToString ()));
							break;
						
						case "width":
							hanglerUrl = hanglerUrl.Replace (enclosedTag, MiniGallerySettings.ThumbWidth.ToString());
							break;

						case "fileid": 
							hanglerUrl = hanglerUrl.Replace (enclosedTag, image.ImageFileID.ToString());
							break;
							
						case "height":
							hanglerUrl = hanglerUrl.Replace (enclosedTag, MiniGallerySettings.ThumbHeight.ToString());
							break;
					}
				}
				
				imageImage.ImageUrl = hanglerUrl;
			}
			
			#region Image size

			if (MiniGallerySettings.ImageWidth.IsEmpty)
			{
				if (!Null.IsNull(MiniGallerySettings.ThumbWidth))
					imageImage.Width = Unit.Pixel(MiniGallerySettings.ThumbWidth);
			}
			else
				imageImage.Width = MiniGallerySettings.ImageWidth;
			
			if (MiniGallerySettings.ImageHeight.IsEmpty)
			{
				if (!Null.IsNull(MiniGallerySettings.ThumbHeight))
					imageImage.Height = Unit.Pixel(MiniGallerySettings.ThumbHeight);
			}
			else
				imageImage.Height = MiniGallerySettings.ImageHeight;
			
			#endregion

			
			
			// NOTE: img width is always 100%, so we don't need this		
			// imageImage.Width = Unit.Parse (settings.ImageWidth);
			// imageImage.Height = Unit.Parse (settings.ImageHeight);
		
			// NOTE: All thumbs stored in filesystem, so we know their size 
			/*
			var thumbFile = FileManager.Instance.GetFile (image.ThumbFileID);

			var width = Unit.Parse (settings.ImageWidth);
			var height = Unit.Parse (settings.ImageHeight);

			imageImage.Width = width.IsEmpty? Unit.Pixel(thumbFile.Width) : width;
			imageImage.Height = height.IsEmpty? Unit.Pixel(thumbFile.Height) : height;
			*/

			// HACK: Titles width hack - title rendering must be done on the client side!
			// e.Item.Width = Unit.Parse (MiniGallerySettings.FrameWidth);
			// e.Item.Height = Unit.Parse (MiniGallerySettings.FrameHeight);

			#endregion

			/*
			#region Title

			labelTitle.Visible = MiniGallerySettings.ShowTitles;

			if (!string.IsNullOrWhiteSpace (image.Title))
				labelTitle.Text = image.Title;
			// THINK: Separate option to hide empty titles?
			// else labelTitle.Visible = false;

			#endregion
             */

			#region Item styles

			e.Item.CssClass = (e.Item.ItemIndex % 2 == 0) ? "MG_Item" : "MG_AltItem";

			if (listImages.RepeatColumns > 0)
			{
				e.Item.CssClass += (e.Item.ItemIndex < listImages.RepeatColumns) ? " MG_ColumnStart" : "";
				e.Item.CssClass += (e.Item.ItemIndex % listImages.RepeatColumns == 0) ? " MG_RowStart dnnClear" : "";
			}

			if (!image.IsPublished)
				e.Item.CssClass += " MG_NotPublished";

			// for testing:
			// labelTitle.Text += " " + (e.Item.ItemIndex + 1) + " " + e.Item.CssClass ;

			#endregion
		}

		#region IActionable implementation

		public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
		{
			get
			{
				// create a new action to add an item, this will be added 
				// to the controls dropdown menu
				var actions = new ModuleActionCollection ();
				
				actions.Add 
				(
					GetNextActionID (), 
					Localization.GetString ("AddImage.Text", LocalResourceFile),
					ModuleActionType.AddContent, 
					"", "", 
					Utils.EditUrl (this, "Edit"), 
					false, 
					DotNetNuke.Security.SecurityAccessLevel.Edit,
					true, 
					false
				);

				actions.Add 
				(
					GetNextActionID (), 
					Localization.GetString ("BulkAddImages.Text", LocalResourceFile),
					ModuleActionType.AddContent, 
					"", "", 
					Utils.EditUrl (this, "BulkAdd"), 
					false, 
					DotNetNuke.Security.SecurityAccessLevel.Edit,
					true, 
					false
				);

				

				return actions;
			}
		}

		#endregion
	}
}
