//
// BulkAdd.cs
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
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.UI.UserControls;
using Telerik.Web.UI;
using DotNetNuke.Entities.Content.Common;

namespace R7.MiniGallery
{
	public partial class BulkAdd : MiniGalleryPortalModuleBase
	{
		#region Handlers

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			// set url for Cancel link
			linkCancel.NavigateUrl = Globals.NavigateURL ();

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
							SortIndex = Utils.TryParseInt32 (textSortIndex.Text, 0),
							IsPublished = true,
							ModuleID = ModuleId,
							CreatedOnDate = now,
							LastModifiedOnDate = now,
							CreatedByUserID = UserId,
							LastModifiedByUserID = UserId
						};

						MiniGalleryController.Add<ImageInfo> (image);
					}
				}
				
				Utils.SynchronizeModule (this);
				
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

			imageImage.ImageUrl = Utils.FormatURL(this, "FileID=" + file.FileId, false);
			checkIsIncluded.Text = file.FileName;
			hiddenImageFileID.Value = file.FileId.ToString ();

            // set default sort index as first number in a filename, multiplied by 10
            textSortIndex.Text = (Utils.ExtractInt32 (Path.GetFileNameWithoutExtension (file.FileName)) * 10).ToString ();
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
	
	}
}

