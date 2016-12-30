//
// ViewMiniGallery.ascx.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Icons;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;

namespace R7.MiniGallery
{
	public partial class ViewMiniGallery : MiniGalleryPortalModuleBase, IActionable
	{
		#region Fields
		
		// TODO: Add more R7.Imagehandler tags 

		private string [] imageHandlerTags = new [] { "fileticket", "width", "fileid", "height" };

		private LightboxBase lightbox;
		
		#endregion

		#region Properties

		public string EditIconUrl
		{
			get { return IconController.IconURL ("Edit"); }
		}

		protected LightboxBase Lightbox
		{
			get
			{
				if (lightbox == null)
					lightbox = LightboxBase.Create (MiniGallerySettings.LightboxType, TabModuleId.ToString());
				
				return lightbox;
			}	
		}

		#endregion

		#region Handlers

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
	
			Lightbox.Register (includeLightboxJs, includeLightboxCss, literalLightboxScript);
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
							listImages.ItemStyle.Width = Unit.Percentage (100.0 / listImages.RepeatColumns);
					}

					// add current style CSS class to the list
					listImages.CssClass += " MG_" + MiniGallerySettings.StyleSet;

					// if (ImageViewer == ImageViewer.YoxView)
					// 	listImages.CssClass += " yoxview";
							
					// set maximum height of a list
					var maxHeight = MiniGallerySettings.MaxHeight;
					if (!maxHeight.IsEmpty)
						listImages.Style.Add ("max-height", maxHeight.ToString());

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

			#region Alternate text and title
            imageImage.AlternateText = (!string.IsNullOrWhiteSpace (image.Alt))? image.Alt : 
                (!string.IsNullOrWhiteSpace (image.Title))? image.Title :
                string.Format (LocalizeString ("AltAuto.Format"), ModuleConfiguration.ModuleTitle, e.Item.ItemIndex);

			imageImage.ToolTip = (!string.IsNullOrWhiteSpace (image.Title))? image.Title : image.Alt;
			
			#endregion
			
			#region Link

			// TODO: url type (secured or none) must be set in settings

			if (!string.IsNullOrWhiteSpace (image.Url))
			{	
				linkImage.NavigateUrl = Utils.FormatURL (this, image.Url, false);
			}
			else
			{
				// no url specified, link to image itself
				linkImage.NavigateUrl = Utils.FormatURL (this, "FileID=" + image.ImageFileID, false);
			}			

			// lightbox
			if (Lightbox.LightboxType != LightboxType.None)
			{
				Lightbox.ApplyTo (imageImage, linkImage);
			}
			else
			{
				// no lightbox, set target
				var target = MiniGallerySettings.Target;
				if (target != "none")
					linkImage.Target = target;
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
				{
					hanglerUrl += "fileticket={fileticket}";

					if (!Null.IsNull (MiniGallerySettings.ThumbWidth))
						hanglerUrl += "&width={width}";

					if (!Null.IsNull (MiniGallerySettings.ThumbHeight))
						hanglerUrl += "&height={height}";
				}

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
					
                imageImage.ImageUrl = Globals.AddHTTP (PortalAlias.HTTPAlias +
                    hanglerUrl + "&ext=." + image.File.Extension.ToLowerInvariant ());
			}
			
			#region Image size

			if (MiniGallerySettings.ImageWidth.IsEmpty && MiniGallerySettings.ImageHeight.IsEmpty)
			{
				if (!Null.IsNull (MiniGallerySettings.ThumbWidth) && !Null.IsNull (MiniGallerySettings.ThumbHeight))
				{
					// If both ThumbWidth & ThumbHeight are not null, produced image dimensions are determined
					// also by ResizeMode image handler param. Default is "Fit" - so, by example, if produced
					// images have same width, height may vary, and vice versa.
				}
				else if (!Null.IsNull (MiniGallerySettings.ThumbWidth))
					imageImage.Width = Unit.Pixel (MiniGallerySettings.ThumbWidth);
				else if (!Null.IsNull (MiniGallerySettings.ThumbHeight))
					imageImage.Height = Unit.Pixel (MiniGallerySettings.ThumbHeight);
			}
			else
			{
				if (!MiniGallerySettings.ImageWidth.IsEmpty)
					imageImage.Width = MiniGallerySettings.ImageWidth;
			
				if (!MiniGallerySettings.ImageHeight.IsEmpty)
					imageImage.Height = MiniGallerySettings.ImageHeight;
			}

			
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

            // mark odd and even items
			e.Item.CssClass = (e.Item.ItemIndex % 2 == 0) ? "MG_Item" : "MG_AltItem";

			if (listImages.RepeatColumns > 0)
			{
                // mark column and row start items
				e.Item.CssClass += (e.Item.ItemIndex < listImages.RepeatColumns) ? " MG_ColumnStart" : "";
				e.Item.CssClass += (e.Item.ItemIndex % listImages.RepeatColumns == 0) ? " MG_RowStart dnnClear" : "";

                // remove right and left margins
                if (MiniGallerySettings.ExpandColumns)
                    e.Item.CssClass += " MG_ExpandColumns";
			}

            // mark not published items
			if (!image.IsPublished)
				e.Item.CssClass += " MG_NotPublished";

			// for testing:
			// labelTitle.Text += " " + (e.Item.ItemIndex + 1) + " " + e.Item.CssClass ;

			#endregion
		}

		#region IActionable implementation

		public ModuleActionCollection ModuleActions
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
					SecurityAccessLevel.Edit,
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
					SecurityAccessLevel.Edit,
					true, 
					false
				);

				return actions;
			}
		}

		#endregion
	}
}
