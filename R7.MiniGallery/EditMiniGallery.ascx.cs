//
//  EditMiniGallery.ascx.cs
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
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using R7.Dnn.Extensions.Modules;
using R7.Dnn.Extensions.Utilities;
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
					// parse querystring parameters on first
					int imageId;
					if (int.TryParse (Request.QueryString ["ImageID"], out imageId))
					{	
                        _image = new MiniGalleryDataProvider ().Get<ImageInfo> (imageId, ModuleId);
					}
					
				}
				return _image;
			}
			set
			{
				_image = value;
			}

		}


		#endregion

		#region Handlers

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);

			// set url for Cancel link
            linkCancel.NavigateUrl = UrlHelper.GetCancelUrl (UrlUtils.InPopUp ());

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
                        datetimeStartDate.SelectedDate = Image.StartDate;
                        datetimeEndDate.SelectedDate = Image.EndDate;

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
					// populate new object properties with data from controls to add new record

					var now = DateTime.Now;

                    var image = new ImageInfo () {
                        Alt = textAlt.Text,
                        Title = textTitle.Text,
                        SortIndex = TypeUtils.ParseToNullable<int> (textSortIndex.Text) ?? 0,
                        ModuleID = ModuleId,
                        Url = urlLink.Url,
                        ImageFileID = pickerImage.FileID,
                        CreatedOnDate = now,
                        LastModifiedOnDate = now,
                        CreatedByUserID = UserId,
                        LastModifiedByUserID = UserId,
                        StartDate = datetimeStartDate.SelectedDate,
                        EndDate = datetimeEndDate.SelectedDate
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
					image.Url = urlLink.Url;
					image.ImageFileID = pickerImage.FileID;
					image.LastModifiedOnDate = DateTime.Now;
					image.LastModifiedByUserID = UserId;
                    image.StartDate = datetimeStartDate.SelectedDate;
                    image.EndDate = datetimeEndDate.SelectedDate;

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

		#endregion
	}
}
