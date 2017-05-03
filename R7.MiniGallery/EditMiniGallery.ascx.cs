//
// EditMiniGallery.ascx.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2017 Roman M. Yagodin
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
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using R7.DotNetNuke.Extensions.Modules;
using R7.DotNetNuke.Extensions.Utilities;
using R7.MiniGallery.Data;
using R7.MiniGallery.Models;

namespace R7.MiniGallery
{
    public partial class EditMiniGallery : PortalModuleBase<MiniGallerySettings>
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
                            _image = new MiniGalleryDataProvider ().Get<ImageInfo> (imageId, ModuleId);
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
			linkCancel.NavigateUrl = UrlHelper.GetCancelUrl (UrlHelper.IsInPopup (Request));

			// setup image picker
			pickerImage.FileFilter = Globals.glbImageFileTypes;

			// event wireup
			buttonUpdate.Click += OnUpdateClick;
			buttonDelete.Click += OnDeleteClick;

			// add confirmation dialog to delete button
			buttonDelete.Attributes.Add ("onClick", "javascript:return confirm('" + Localization.GetString ("DeleteItem") + "');");
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

                        pickerImage.FileID = Image.ImageFileID;
							
						// setup audit control
						ctlAudit.CreatedDate = 
							Image.CreatedOnDate.ToShortDateString () + " " +
						Image.CreatedOnDate.ToLongTimeString ();
						ctlAudit.LastModifiedDate = 
							Image.LastModifiedOnDate.ToShortDateString () + " " +
						Image.LastModifiedOnDate.ToLongTimeString ();
						ctlAudit.CreatedByUser = Image.CreatedByUserName;
						ctlAudit.LastModifiedByUser = Image.LastModifiedByUserName;

						// UpdatePreview ();
					}
					else if (Request.QueryString ["ImageID"] != null)
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
                var dataProvider = new MiniGalleryDataProvider ();

				// determine if we are adding or updating
				// ALT: if (Null.IsNull (itemId))
				if (Image == null)
				{
					// populate new object properties with data from controls 
					// to add new record

					var now = DateTime.Now;

					var image = new ImageInfo () {
						Alt = textAlt.Text,
						Title = textTitle.Text,
                        SortIndex = TypeUtils.ParseToNullable<int> (textSortIndex.Text) ?? 0,
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

					dataProvider.Add<ImageInfo> (image);
				}
				else
				{
					// update properties of existing object with data from controls 
					// to update existing record
					// image = ctrl.Get<ImageInfo> (imageId.Value, ModuleId);
					var image = Image;
					image.Alt = textAlt.Text;
					image.Title = textTitle.Text;
                    image.SortIndex = TypeUtils.ParseToNullable<int> (textSortIndex.Text) ?? Image.SortIndex;
					// image.ModuleID = ModuleId;
					image.Url = urlLink.Url;
					//image.ThumbFileID = int.Parse (urlImage.Url.Replace ("FileID=", ""));
					image.ImageFileID = pickerImage.FileID;
					image.LastModifiedOnDate = DateTime.Now;
					image.LastModifiedByUserID = UserId;
					image.IsPublished = checkIsPublished.Checked;

					dataProvider.Update<ImageInfo> (image);
				}

                CacheHelper.RemoveCacheByPrefix ("//r7_MiniGallery");
                ModuleController.SynchronizeModule (ModuleId);

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
                    new MiniGalleryDataProvider ().Delete<ImageInfo> (Image);

                    CacheHelper.RemoveCacheByPrefix ("//r7_MiniGallery");
                    ModuleController.SynchronizeModule (ModuleId);

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

