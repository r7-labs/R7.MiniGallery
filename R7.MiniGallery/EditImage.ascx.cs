using System;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Localization;
using R7.Dnn.Extensions.FileSystem;
using R7.Dnn.Extensions.Modules;
using R7.Dnn.Extensions.Text;
using R7.Dnn.Extensions.Urls;
using R7.MiniGallery.Data;
using R7.MiniGallery.Models;
using R7.University.Components;

namespace R7.MiniGallery
{
    public partial class EditImage : PortalModuleBase<MiniGallerySettings>
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
                        _image = new MiniGalleryDataProvider ().Get<ImageInfo,int,int> (imageId, ModuleId);
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
            pickerImage.FolderPath = GetImagesFolderPath ();

            // event wireup
            buttonUpdate.Click += OnUpdateClick;
			buttonDelete.Click += OnDeleteClick;
            btnDeleteWithFile.Click += OnDeleteClick;

            // add confirmation dialog to delete button
            buttonDelete.Attributes.Add ("onClick", "javascript:return confirm('" + Localization.GetString ("DeleteItem") + "');");
            btnDeleteWithFile.Attributes.Add ("onClick", "javascript:return confirm('" + LocalizeString ("btnDeleteWithFile_Confirm.Text") + "');");
        }

        string GetImagesFolderPath ()
        {
            var folderId = FolderHistory.GetLastFolderId (Request, PortalId);
            if (folderId != null) {
                var folder = FolderManager.Instance.GetFolder (folderId.Value);
                if (folder != null) {
                    return folder.FolderPath;
                }
            }

            return string.Empty;
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
                        chkOpenInLightbox.Checked = Image.OpenInLightbox;
                        txtCssClass.Text = Image.CssClass;

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
                        var dataProvider = new MiniGalleryDataProvider ();
                        textSortIndex.Text = (dataProvider.GetBaseSortIndex (ModuleId) + MiniGalleryConfig.Instance.SortIndexStep).ToString ();

						buttonDelete.Visible = false;
                        btnDeleteWithFile.Visible = false;
                        ctlAudit.Visible = false;
					}
				}
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

        protected void OnUpdateClick (object sender, EventArgs e)
		{
			try
			{
                var now = DateTime.Now;
                var addNew = Image == null;
                var image = default (ImageInfo);

				if (addNew)
                {
					image = new ImageInfo {
                        SortIndex = ParseHelper.ParseToNullable<int> (textSortIndex.Text) ?? 0,
                        ModuleID = ModuleId,
                        CreatedOnDate = now,
                        CreatedByUserID = UserId
                    };
                }
				else
				{
					image = Image;
                    image.SortIndex = ParseHelper.ParseToNullable<int> (textSortIndex.Text) ?? Image.SortIndex;
                }

                image.ImageFileID = pickerImage.FileID;
                image.Alt = textAlt.Text.Trim ();
                image.Title = textTitle.Text.Trim ();
                image.Url = urlLink.Url;
                image.OpenInLightbox = chkOpenInLightbox.Checked;
                image.CssClass = txtCssClass.Text.Trim ();
                image.StartDate = datetimeStartDate.SelectedDate;
                image.EndDate = datetimeEndDate.SelectedDate;
                image.LastModifiedOnDate = DateTime.Now;
                image.LastModifiedByUserID = UserId;

                var dataProvider = new MiniGalleryDataProvider ();
                if (addNew) {
                    dataProvider.Add (image);
                }
                else {
                    dataProvider.Update (image);
                }

                DataCache.ClearCache ("//r7_MiniGallery");
                ModuleController.SynchronizeModule (ModuleId);

                RememberFolder (image.ImageFileID);

				Response.Redirect (Globals.NavigateURL (), true);
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

        void RememberFolder (int imageFileId)
        {
            if (imageFileId > 0) {
                var file = FileManager.Instance.GetFile (imageFileId);
                if (file != null) {
                    var folder = FolderManager.Instance.GetFolder (file.FolderId);
                    if (folder != null) {
                        FolderHistory.RememberFolder (Request, Response, folder.FolderID, PortalId);
                    }
                }
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
                    var dataProvider = new MiniGalleryDataProvider ();
                    dataProvider.Delete (Image);

                    if (sender == btnDeleteWithFile) {
                        dataProvider.DeleteImageFile (Image);
                    }

                    DataCache.ClearCache ("//r7_MiniGallery");
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
