/*using System;
using System.Web.UI.WebControls;
using System.Linq;

using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.UserControls;

namespace Redhound.MiniGallery
{
	public partial class EditMiniGallery : PortalModuleBase
	{
		// ALT: private int itemId = Null.NullInteger;
		private int? itemId = null; 
		
		#region Handlers
		
		/// <summary>
		/// Handles Page_Load event for a control
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void Page_Load (object sender, EventArgs e)
		{
			try {
				// parse querystring parameters
				int tmpItemId;
				if (int.TryParse (Request.QueryString ["MiniGalleryID"], out tmpItemId))
					itemId = tmpItemId;
				
				if (!IsPostBack) {
					// load the data into the control the first time we hit this page
					
					cmdDelete.Attributes.Add ("onClick", "javascript:return confirm('" + Localization.GetString ("DeleteItem") + "');");
					
					// check we have an item to lookup
					// ALT: if (!Null.IsNull (itemId) 
					if (itemId.HasValue) {
						// load the item
						var ctrl = new MiniGalleryController ();
						var item = ctrl.Get<MiniGalleryInfo> (itemId.Value, this.ModuleId);
						
						if (item != null) {
							// TODO: Fill controls with data
							txtContent.Text = item.Content;
							
							// setup audit control
							ctlAudit.CreatedByUser = item.CreatedByUserName;
							ctlAudit.CreatedDate = item.CreatedOnDate.ToLongDateString ();
						} else
							Response.Redirect (Globals.NavigateURL (), true);
					} else {
						cmdDelete.Visible = false;
						ctlAudit.Visible = false;
					}
				}
			} catch (Exception ex) {
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
		protected void cmdUpdate_Click (object sender, EventArgs e)
		{
			try {
				var ctrl = new MiniGalleryController ();
				MiniGalleryInfo item;
				
				// determine if we are adding or updating
				// ALT: if (Null.IsNull (itemId))
				if (!itemId.HasValue) {
					// TODO: populate new object properties with data from controls 
					// to add new record
					item = new MiniGalleryInfo ();
					item.Content = txtContent.Text;
					item.ModuleID = this.ModuleId;
					item.CreatedByUser = this.UserId;					
					
					ctrl.Add<MiniGalleryInfo> (item);
				} else {
					// TODO: update properties of existing object with data from controls 
					// to update existing record
					item = ctrl.Get<MiniGalleryInfo> (itemId.Value, this.ModuleId);
					item.Content = txtContent.Text;
					
					ctrl.Update<MiniGalleryInfo> (item);
				}
				
				Response.Redirect (Globals.NavigateURL (), true);
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
		
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
			try {
				Response.Redirect (Globals.NavigateURL (), true);
			} catch (Exception ex) {
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
		protected void cmdDelete_Click (object sender, EventArgs e)
		{
			try {
				// ALT: if (!Null.IsNull (itemId))
				if (itemId.HasValue) {
					var ctrl = new MiniGalleryController ();
					ctrl.Delete<MiniGalleryInfo> (itemId.Value);
					Response.Redirect (Globals.NavigateURL (), true);
				}
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
		
		#endregion
	}
}*/

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

namespace R7.MiniGallery
{
	// TODO: More adequate name for Import control
	// compare thumb1 & thumb2 => filter to thumbs
	// compare thumb1 & file1 => filter to files


	public partial class BulkAdd : MiniGalleryPortalModuleBase
	{
		#region Handlers

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			// set url for Cancel link
			linkCancel.NavigateUrl = Globals.NavigateURL ();

			ddlFolders.SelectionChanged += dllFolders_SelectionChanged;
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

			try
			{
				/*
				if (DotNetNuke.Framework.AJAX.IsInstalled())
				{
					DotNetNuke.Framework.AJAX.RegisterScriptManager();
					var sm = DotNetNuke.Framework.AJAX.GetScriptManager(this.Page);
					sm.RegisterAsyncPostBackControl(listFiles);
				}*/

				if (!IsPostBack)
				{
					// add root folder in the top of the list
					//var rootFolder = FolderManager.Instance.GetFolder(PortalId, string.Empty);
					//ddlFolder.Items.Add(new ListItem(
					//	Localization.GetString("PortalRoot.Text", LocalResourceFile), rootFolder.FolderID.ToString()));
					//ddlFolder.SelectedIndex = 0;

					/*foreach (var folder in FolderManager.Instance.GetFolders(PortalId).OrderBy(f => f.FolderPath))
						ddlFolder.Items.Add (new ListItem (folder.FolderPath, folder.FolderID.ToString ()));

					// set text for portal root folder
					ddlFolder.Items [0].Text = Localization.GetString ("PortalRoot.Text", LocalResourceFile);*/
				}
				else
				{

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
		protected void buttonUpdate_Click (object sender, EventArgs e)
		{
			try
			{
			/*	var now = DateTime.Now;
				var sortIndex = 10;

				foreach (DataListItem item in listPairs.Items)
				{
					var checkThumb = item.FindControl ("checkThumb") as CheckBox;
					var hiddenThumb = item.FindControl ("hiddenThumb") as HiddenField;
					var ddlFiles = item.FindControl ("ddlFiles") as DropDownList;

					var dataItem = item.DataItem as Tuple<IFileInfo, IFileInfo>;

					// add only selected items
					if (checkThumb.Checked)
					{
						var image = new ImageInfo () {
							ImageFileID = int.Parse (hiddenThumb.Value),
							Alt = "",
							Title = "",
							Url = "FileID=" + ddlFiles.SelectedValue,
							SortIndex = sortIndex += 10,
							IsPublished = true,
							ModuleID = ModuleId,
							CreatedOnDate = now,
							LastModifiedOnDate = now,
							CreatedByUserID = UserId,
							LastModifiedByUserID = UserId
						};

						MiniGalleryController.Add<ImageInfo> (image);
					}
				}*/

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

			listImages.DataSource = files;
			listImages.DataBind ();
		}

		protected void ddlThumbFilter_SelectedIndexChanged (object sender, EventArgs e)
		{
			/* if (ddlThumbFilter.SelectedIndex < 2)
			{
				textCustomThumbFilter.Text = ddlThumbFilter.SelectedValue;
				divCustomThumbFilter.Visible = false;
			}
			else
			{
				textCustomThumbFilter.Text = ".+[_-]+(suffix1|suffix2)";
				divCustomThumbFilter.Visible = true;
			}

			MakePairs (); */
		}

		protected void buttonApplyFilter_Click (object sender, EventArgs e)
		{
		//	MakePairs ();
		}
		/*
		protected void MakePairs ()
		{
			if (ddlFolder.SelectedIndex >= 0)
			{
				
				var folderId = Utils.TryParseInt32 (ddlFolder.SelectedValue, Null.NullInteger);
				
				if (!Null.IsNull (folderId))
				{
					var folder = FolderManager.Instance.GetFolder (folderId);	
					
					// use one of the 2 default filter regexes or a user-defined one
					var filterRegex = (ddlThumbFilter.SelectedIndex < 2) ? 
						ddlThumbFilter.SelectedItem.Value : textCustomThumbFilter.Text;

					// lists for thumbs and other files
					var thumbs = new List<IFileInfo> ();
					var files = new List<IFileInfo> ();
				
					// separate thumbs from other files, using filter regex
					foreach (var file in FolderManager.Instance.GetFiles(folder))
					{
						if (Regex.IsMatch (
							    Path.GetFileNameWithoutExtension (file.FileName), 
							    filterRegex, RegexOptions.IgnoreCase))
							thumbs.Add (file);
						else
							files.Add (file);
					}

					// pass this var to listPairs_ItemDataBound
					tmpFiles = new List<IFileInfo> (files);

					// make a list of IFileInfo pairs
					var pairs = new List<Tuple<IFileInfo, IFileInfo>> ();

					// assume pairs
					foreach (var thumb in thumbs)
						for (var i = 0; i < files.Count; i++)
						{ 
							var thumbShort = Path.GetFileNameWithoutExtension (thumb.FileName).ToLowerInvariant ();
							var fileShort = Path.GetFileNameWithoutExtension (files [i].FileName).ToLowerInvariant ();

							// the thumb filename must be a substring of target filename
							if (thumbShort.Contains (fileShort))
							{
								pairs.Add (new Tuple<IFileInfo,IFileInfo> (thumb, files [i]));
								files.RemoveAt (i);
								break;
							}
						}

					// bound pairs to the datalist
					listPairs.DataSource = pairs;
					listPairs.DataBind ();
					// if no pairs, list are empty (it's usefull)

					// define Update button visibility
					buttonUpdate.Visible = pairs.Count > 0;
				}
			}
		}
*/
		private List<IFileInfo> tmpFiles;

		protected void listImages_ItemDataBound (object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			var file = e.Item.DataItem as IFileInfo;

			var imageImage = e.Item.FindControl ("imageImage") as Image;
			var checkIsIncluded = e.Item.FindControl ("checkIsIncluded") as CheckBox;
			var textAlt = e.Item.FindControl ("textAlt") as TextBox;
			var textSortIndex = e.Item.FindControl ("textSortIndex") as TextBox;
			
			imageImage.ImageUrl = Utils.FormatURL(this, "FileID=" + file.FileId, false);
			checkIsIncluded.Text = file.FileName;
		}

		#endregion
	
	}
}

