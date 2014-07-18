using System;
using System.Web.UI.WebControls;
using System.Linq;

using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.UserControls;

// TODO: Update imagePreview then new file in urlImage is selected 

namespace R7.MiniGallery
{
	public partial class EditMiniGallery : MiniGalleryPortalModuleBase
	{
		#region Properties

		private ImageInfo _image;

		// TODO: ImageInfo must be Serializable

		private ImageInfo Image 
		{
			get 
			{
				if (_image == null)
				{
					//if (!IsPostBack)
					{
						// parse querystring parameters on first
						int imageId;
						if (int.TryParse (Request.QueryString ["ImageID"], out imageId))
						{	
							_image = MiniGalleryController.Get<ImageInfo> (imageId, ModuleId);
							/*if (_image != null)
								ViewState["Image"] = _image;*/
						}
					}
					/*else
					{
						// it's a postback, so image must be in a viewstate
						if (ViewState["Image"] != null)
							_image = (ImageInfo)ViewState["Image"];
					}*/

				}
				return _image;
			}
			set 
			{
				_image = value;
				//ViewState["Image"] = _image;
			}

		}


		#endregion

		#region Handlers

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);

			// set url for Cancel link
			linkCancel.NavigateUrl = Globals.NavigateURL ();

			// event wireup
			buttonUpdate.Click += OnUpdateClick;
			buttonDelete.Click += OnDeleteClick;

		}

		/// <summary>
		/// Handles the load event.
		/// </summary>
		/// <param name="e">Event args</param>
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);

			try
			{
				if (!IsPostBack)
				{
					// load the data into the control the first time we hit this page

					// check if we have an item
					if (Image != null)
					{
						// fill controls with data
						textAlt.Text = Image.Alt;
						textTitle.Text = Image.Title;
						textSortIndex.Text = Image.SortIndex.ToString ();
						checkIsPublished.Checked = Image.IsPublished;



						/*
						 * //labelTest.Text = PortalSettings.HomeDirectoryMapPath + FileManager.Instance.GetFile(Image.ThumbFileID).Folder;
						pickerImage.FolderPath = 
							PortalSettings.HomeDirectoryMapPath + 
							FileManager.Instance.GetFile(Image.ThumbFileID).Folder;


						labelTest.Text = pickerImage.FilePath = 
							PortalSettings.HomeDirectoryMapPath + 
								FileManager.Instance.GetFile(Image.ThumbFileID).Folder +
								FileManager.Instance.GetFile(Image.ThumbFileID).FileName;

						pickerImage.FileID = Image.ThumbFileID;
						*/
						urlLink.Url = Image.Url;

						pickerImage.FileFilter = Globals.glbImageFileTypes;

						// make portal-relative path
						// form url like /portals/0/common/121325/image.jpg
						pickerImage.FilePath = FileManager.Instance.GetUrl(
							FileManager.Instance.GetFile(Image.ImageFileID))
							.Remove(0, PortalSettings.HomeDirectory.Length);
							
						// setup audit control
						ctlAudit.CreatedDate = 
							Image.CreatedOnDate.ToShortDateString () + " " + 
							Image.CreatedOnDate.ToLongTimeString();
						ctlAudit.LastModifiedDate = 
							Image.LastModifiedOnDate.ToShortDateString() + " " +
							Image.LastModifiedOnDate.ToLongTimeString();
						ctlAudit.CreatedByUser = Image.CreatedByUserName;
						ctlAudit.LastModifiedByUser = Image.LastModifiedByUserName;

						// UpdatePreview ();
					}
					else if (Request.QueryString["ImageID"] != null)
					{
						// no image with ImageID=x was found in a DB
						Response.Redirect (Globals.NavigateURL (), true);
					}
					else
					{
						// new image
						buttonDelete.Visible = false;
						ctlAudit.Visible = false;

						// UpdatePreview ();
					}				
				}
				else // in postback
				{
				//	labelTest.Text = pickerImage.FilePath;
					// UpdatePreview();
				}
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		/// <summary>
		/// Handles Click event for cmdUpdate button
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void OnUpdateClick (object sender, EventArgs e)
		{
			try
			{
				// ImageInfo image;

				// determine if we are adding or updating
				// ALT: if (Null.IsNull (itemId))
				if (Image == null)
				{
					// populate new object properties with data from controls 
					// to add new record

					 var now = DateTime.Now;

					 var image = new ImageInfo () 
					 {
						Alt = textAlt.Text,
						Title = textTitle.Text,
						SortIndex = Utils.TryParseInt32(textSortIndex.Text, 1),
						ModuleID = ModuleId,
						Url = urlLink.Url,
						//ThumbFileID = int.Parse(urlImage.Url.Replace ("FileID=", "")),
						ImageFileID = pickerImage.FileID,
						CreatedOnDate = now,
						LastModifiedOnDate = now,
						CreatedByUserID = UserId,
						LastModifiedByUserID = UserId,
						IsPublished = checkIsPublished.Checked
					 };					

					MiniGalleryController.Add<ImageInfo> (image);
				}
				else
				{
					// update properties of existing object with data from controls 
					// to update existing record
					// image = ctrl.Get<ImageInfo> (imageId.Value, ModuleId);
					var image = Image;
					image.Alt = textAlt.Text;
					image.Title = textTitle.Text;
					image.SortIndex = Utils.TryParseInt32 (textSortIndex.Text, 10);
					// image.ModuleID = ModuleId;
					image.Url = urlLink.Url;
					//image.ThumbFileID = int.Parse (urlImage.Url.Replace ("FileID=", ""));
					image.ImageFileID = pickerImage.FileID;
					image.LastModifiedOnDate = DateTime.Now;
					image.LastModifiedByUserID = UserId;
					image.IsPublished = checkIsPublished.Checked;

					MiniGalleryController.Update<ImageInfo> (image);
				}

				Response.Redirect (Globals.NavigateURL (), true);
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		/*
		/// <summary>
		/// Handles Click event for cmdCancel button
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void cmdCancel_Click (object sender, EventArgs e)
		{
			try 
			{
				Response.Redirect (Globals.NavigateURL (), true);
			} 
			catch (Exception ex) 
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}*/

		/// <summary>
		/// Handles Click event for cmdDelete button
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void OnDeleteClick (object sender, EventArgs e)
		{
			try
			{
				if (Image != null)
				{
					MiniGalleryController.Delete<ImageInfo> (Image);

					Response.Redirect (Globals.NavigateURL (), true);
				}
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		/*
		protected void buttonUpdatePreview_Click(object sender, EventArgs e)
		{
			UpdatePreview ();
		}

		private void UpdatePreview()
		{
			bool showImage =  !string.IsNullOrWhiteSpace(urlImage.Url);
			imagePreview.ImageUrl = (showImage)? Utils.FormatURL (this, urlImage.Url, false) : string.Empty;
		}*/

		#endregion
	
	}
}

