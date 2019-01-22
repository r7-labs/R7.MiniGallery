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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using R7.Dnn.Extensions.Modules;
using R7.Dnn.Extensions.Text;
using R7.Dnn.Extensions.Urls;
using R7.Dnn.Extensions.Utilities;
using R7.MiniGallery.Data;
using R7.MiniGallery.Models;

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
			
			/*
			try
			{
				if (!IsPostBack)
				{
				}
				else
				{
				}
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}*/
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

                var dataProvider = new MiniGalleryDataProvider ();
				foreach (DataListItem item in listImages.Items)
				{
					var checkIsIncluded = item.FindControl ("checkIsIncluded") as CheckBox;
					var textTitle = item.FindControl ("textTitle") as TextBox;
					var textSortIndex = item.FindControl ("textSortIndex") as TextBox;
					var hiddenImageFileID = item.FindControl ("hiddenImageFileID") as HiddenField;

					// add only selected items
					if (checkIsIncluded.Checked)
					{
						var image = new ImageInfo () {
							ImageFileID = int.Parse (hiddenImageFileID.Value),
                            Alt = string.Empty, // title value should be used for Alt dynamically in the View
							Title = textTitle.Text,
							Url = string.Empty,
                            SortIndex = ParseHelper.ParseToNullable<int> (textSortIndex.Text) ?? 0,
							ModuleID = ModuleId,
							CreatedOnDate = now,
							LastModifiedOnDate = now,
							CreatedByUserID = UserId,
							LastModifiedByUserID = UserId
						};

                        dataProvider.Add<ImageInfo> (image);
					}
				}
				
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
			var textSortIndex = e.Item.FindControl ("textSortIndex") as TextBox;
			var hiddenImageFileID = e.Item.FindControl ("hiddenImageFileID") as HiddenField;

            // FIXME: Localize tooltips
            textTitle.ToolTip = LocalizeString ("textTitle.ToolTip");
            textSortIndex.ToolTip = LocalizeString ("textSortIndex.ToolTip");

            imageImage.ImageUrl = Globals.LinkClick ("FileID=" + file.FileId, TabId, ModuleId, false);
			checkIsIncluded.Text = file.FileName;
			hiddenImageFileID.Value = file.FileId.ToString ();

            // set default sort index as first number in a filename, multiplied by 10
            textSortIndex.Text = (ExtractInt32 (Path.GetFileNameWithoutExtension (file.FileName)) * 10).ToString ();
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
	
        private int ExtractInt32 (string text, int defaultValue = default (int))
        {
            var matches = Regex.Matches (text, @"\d+");
            if (matches != null && matches.Count > 0) {
                int result;
                if (int.TryParse (matches [0].Value, out result))
                    return result;
            }

            return defaultValue;
        }
	}
}

