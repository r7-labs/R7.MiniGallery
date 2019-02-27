//
//  BulkAdd.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014-2019 Roman M. Yagodin
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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using R7.Dnn.Extensions.FileSystem;
using R7.Dnn.Extensions.Modules;
using R7.Dnn.Extensions.Text;
using R7.Dnn.Extensions.Urls;
using R7.MiniGallery.Data;
using R7.MiniGallery.Models;
using R7.University.Components;

namespace R7.MiniGallery
{
    public partial class BulkAdd : PortalModuleBase<MiniGallerySettings>
	{
		#region Handlers

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);

            // set url for Cancel link
            linkCancel.NavigateUrl = UrlHelper.GetCancelUrl (UrlUtils.InPopUp ());

			// wireup handlers
			ddlFolders.SelectionChanged += dllFolders_SelectionChanged;
			buttonCheckAll.Click += buttonChecks_Click;
			buttonUncheckAll.Click += buttonChecks_Click;
			buttonInvertSelection.Click += buttonChecks_Click;
		}

		/// <summary>
		/// Handles Page_Load event for a control
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			
			try {
				if (!IsPostBack) {
                    var folderId = FolderHistory.GetLastFolderId (Request, PortalId);
                    if (folderId != null) {
                        var folder = FolderManager.Instance.GetFolder (folderId.Value);
                        if (folder != null) {
                            ddlFolders.SelectedFolder = folder;
                            dllFolders_SelectionChanged (ddlFolders, new EventArgs ());
                        }
                    }
				}
			}
			catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		/// <summary>
		/// Handles Click event for cmdAdd button
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void buttonAdd_Click (object sender, EventArgs e)
		{
			try
			{
				var now = DateTime.Now;
                var images = new List<ImageToAdd> ();

                foreach (DataListItem item in listImages.Items) {
                    var checkIsIncluded = item.FindControl ("checkIsIncluded") as CheckBox;
					var textTitle = item.FindControl ("textTitle") as TextBox;
					var textOrder = item.FindControl ("textOrder") as TextBox;
					var hiddenImageFileID = item.FindControl ("hiddenImageFileID") as HiddenField;

					// add only selected items
					if (checkIsIncluded.Checked)
					{
                        var image = new ImageToAdd {
                            ImageFileID = int.Parse (hiddenImageFileID.Value),
                            FileName = checkIsIncluded.Text,
                            Title = textTitle.Text,							
                            Order = ParseHelper.ParseToNullable<int> (textOrder.Text) ?? int.MaxValue,
							
						};
                        images.Add (image);
					}
				}

                var dataProvider = new MiniGalleryDataProvider ();
                var sortIndex = dataProvider.GetBaseSortIndex (ModuleId);

                foreach (var image in images.OrderBy (i => i.Order).ThenBy (i => i.FileName)) {
                    sortIndex += MiniGalleryConfig.Instance.SortIndexStep;

                    var img = image.ToImageInfo ();
                    img.SortIndex = sortIndex;
                    img.Url = string.Empty;
                    img.Alt = string.Empty;
                    img.ModuleID = ModuleId;
                    img.CreatedOnDate = now;
                    img.LastModifiedOnDate = now;
                    img.CreatedByUserID = UserId;
                    img.LastModifiedByUserID = UserId;

                    dataProvider.Add (img);
                }

                FolderHistory.RememberFolder (Request, Response, ddlFolders.SelectedFolder.FolderID, PortalId);

                DataCache.ClearCache ("//r7_MiniGallery");
                ModuleController.SynchronizeModule (ModuleId);
				
				Response.Redirect (Globals.NavigateURL (), true);
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

        protected void dllFolders_SelectionChanged (object sender, EventArgs e)
		{
			var folder = ddlFolders.SelectedFolder;
			var files = FolderManager.Instance.GetFiles (folder);
			
			files = files.Where (file => 
				Globals.glbImageFileTypes.Contains (file.Extension.ToLowerInvariant ()))
				.OrderBy (file => file.FileName);

			if (files.Any ())
			{
				listImages.DataSource = files;
				listImages.DataBind ();

				buttonAdd.Visible = true;
				panelCheck.Visible = true;
			}
			else
			{
				buttonAdd.Visible = false;
				panelCheck.Visible = false;
			}
		}

		protected void listImages_ItemDataBound (object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			var file = e.Item.DataItem as IFileInfo;

			var imageImage = e.Item.FindControl ("imageImage") as Image;
			var checkIsIncluded = e.Item.FindControl ("checkIsIncluded") as CheckBox;
			var textTitle = e.Item.FindControl ("textTitle") as TextBox;
			var textOrder = e.Item.FindControl ("textOrder") as TextBox;
			var hiddenImageFileID = e.Item.FindControl ("hiddenImageFileID") as HiddenField;

            textTitle.ToolTip = LocalizeString ("textTitle.ToolTip");
            textOrder.ToolTip = LocalizeString ("textOrder.ToolTip");

            imageImage.ImageUrl = Globals.LinkClick ("FileID=" + file.FileId, TabId, ModuleId, false);
			checkIsIncluded.Text = file.FileName;
			hiddenImageFileID.Value = file.FileId.ToString ();
        }

		protected void buttonChecks_Click (object sender, EventArgs e)
		{
			try
			{
				foreach (DataListItem item in listImages.Items)
				{
					var checkIsIncluded = item.FindControl ("checkIsIncluded") as CheckBox;
					
					if (sender == buttonCheckAll)
						checkIsIncluded.Checked = true;
					else if (sender == buttonUncheckAll)
						checkIsIncluded.Checked = false;
					else if (sender == buttonInvertSelection)
						checkIsIncluded.Checked = !checkIsIncluded.Checked;
					else 
						break;
				}
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		#endregion
	
        private int TryExtractInt32 (string text, int defaultValue = default (int))
        {
            var matches = Regex.Matches (text, @"\d+");
            if (matches != null && matches.Count > 0) {
                if (int.TryParse (matches [0].Value, out int result))
                    return result;
            }

            return defaultValue;
        }
	}
}

