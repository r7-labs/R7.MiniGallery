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
		#region Properties 
		
		public string EditIconUrl
		{
			get { return IconController.IconURL ("Edit"); }
		}

		#endregion

    	#region Handlers 

		/*
		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);
		}
        */

		private ImageViewer ImageViewer = ImageViewer.LightBox2;

		/// <summary>
		/// Handles Page_Load event for a control
		/// </summary>
		/// <param name="e">Event args.</param>
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad(e);

			try 
			{
				if (!IsPostBack) 
				{
					// number of columns
					// NOTE: Setting columns to auto need also special styleset!
					if (MiniGallerySettings.Columns > 0)
					{	
						listImages.RepeatColumns = MiniGallerySettings.Columns;
						if (MiniGallerySettings.Expand)
							listImages.ItemStyle.Width = Unit.Percentage(100 / listImages.RepeatColumns);
					}

					listImages.CssClass += " MG_" + MiniGallerySettings.StyleSet;

					if (ImageViewer == ImageViewer.YoxView)
						listImages.CssClass += " yoxview";
							
					// set maximum height of a list
					var maxHeight = MiniGallerySettings.MaxHeight;
					if (maxHeight >= 0)
						listImages.Style.Add ("max-height", maxHeight.ToString() + "px");

					//listImages.RepeatLayout = RepeatLayout.Flow;

					// get images
					// if settings.Row <=0, all files displayed

					var topn = (MiniGallerySettings.Columns == Null.NullInteger || MiniGallerySettings.Rows == Null.NullInteger)? 
					           0 : MiniGallerySettings.Columns * MiniGallerySettings.Rows;

					var items = MiniGalleryController.GetImagesTopN (ModuleId, IsEditable, true, topn);
				
					// check if we have some content to display, 
					// otherwise display a sample default content from the resources
					if (items.Count == 0 && IsEditable) 
						Utils.Message(this, "AddImages.Help", MessageType.Info, true);
			
//					if (listImages.RepeatColumns % 2 == 0 && items.Count % 2 == 1)
//						items.Add(new ImageInfo() {ThumbFileID = Null.NullInteger} );

					// bind the data
					//n = 0;
					listImages.DataSource = items;
					listImages.DataBind ();
				}
			} catch (Exception ex) 
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
		
		#endregion		
			
        #region IActionable implementation
        
		public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions {
			get {
				// create a new action to add an item, this will be added 
				// to the controls dropdown menu
				var actions = new ModuleActionCollection ();

				actions.Add 
				(
					GetNextActionID (), 
					Localization.GetString ("AddMultipleImages.Text", LocalResourceFile),
                    ModuleActionType.AddContent, 
                    "", "", 
                    Utils.EditUrl (this, "Filter"), 
                    false, 
                    DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, 
                    false
				);

				actions.Add 
				(
					GetNextActionID (), 
					Localization.GetString ("AddSingleImage.Text", LocalResourceFile),
					ModuleActionType.AddContent, 
					"", "", 
					Utils.EditUrl (this, "Edit"), 
					false, 
					DotNetNuke.Security.SecurityAccessLevel.Edit,
					true, 
					false
				);

				return actions;
			}
		}

        #endregion



		/// <summary>
		/// Handles the items being bound to the datalist control. In this method we merge the data with the
		/// template defined for this control to produce the result to display to the user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void listImages_ItemDataBound (object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			// use e.Item.DataItem as object of MiniGalleryInfo class,
			// as we really know it is:
			var image = e.Item.DataItem as ImageInfo;

			// find controls in DataList item template
			var labelTitle = e.Item.FindControl ("labelTitle") as Label;
			var imageImage = e.Item.FindControl ("imageImage") as Image;
			var linkImage = e.Item.FindControl ("linkImage") as HyperLink;
			var linkEdit = e.Item.FindControl ("linkEdit") as HyperLink;
            
			// fill out the controls

			#region Link

			// TODO: url type (secured or none) must be set in settings
			linkImage.NavigateUrl = Utils.FormatURL (this, image.Url, false);

			var target = MiniGallerySettings.Target;
			if (target != "none")
				linkImage.Target = target;

			if (ImageViewer == ImageViewer.LightBox2)
			{
				linkImage.Attributes.Add("data-lightbox","module_" + ModuleId);
				linkImage.Attributes.Remove("target");
			}

			#endregion

			#region Edit Link

			linkEdit.NavigateUrl = Utils.EditUrl (this, "Edit", "ImageID", image.ImageID.ToString());

				// ModuleContext.NavigateUrl (TabId, "Edit", false, "mid", ModuleId.ToString(), "ImageID", image.ImageID.ToString());

			// without popups support:
			// linkEdit.NavigateUrl = EditUrl (TabId, "Edit",  false, "mid", ModuleId.ToString(), "ImageID", image.ImageID.ToString());

			#endregion

			#region Image

			// imageImage.ImageUrl = Utils.FormatURL (this, "FileID=" + image.ImageFileID, false);
			imageImage.ImageUrl = string.Format("/imagehandler.ashx?fileid={0}&width={1}",
				image.ImageFileID, MiniGallerySettings.ImageWidth.Replace("px",""));
			imageImage.ToolTip = image.Title;
			imageImage.AlternateText = image.Alt;

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
			e.Item.Width = Unit.Parse (MiniGallerySettings.ImageWidth);
			e.Item.Height = Unit.Parse (MiniGallerySettings.ImageHeight);

			#endregion

			#region Title

			labelTitle.Visible = MiniGallerySettings.ShowTitles;

			if (!string.IsNullOrWhiteSpace(image.Title))
				labelTitle.Text = image.Title;
			// THINK: Separate option to hide empty titles?
			// else labelTitle.Visible = false;

			#endregion

			#region Item styles

			e.Item.CssClass = (e.Item.ItemIndex % 2 == 0)? "MG_Item" : "MG_AltItem";

			if (listImages.RepeatColumns > 0)
			{
				e.Item.CssClass += (e.Item.ItemIndex < listImages.RepeatColumns)? " MG_ColumnStart" : "";
				e.Item.CssClass += (e.Item.ItemIndex % listImages.RepeatColumns == 0)? " MG_RowStart dnnClear" : "";
			}

			if (!image.IsPublished)
				e.Item.CssClass += " MG_NotPublished";

			// for testing:
			// labelTitle.Text += " " + (e.Item.ItemIndex + 1) + " " + e.Item.CssClass ;

			#endregion

		}
	}
}

