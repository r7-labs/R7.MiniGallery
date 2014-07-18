using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Linq;

using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.UserControls;
using DotNetNuke.Common.Utilities;

namespace R7.MiniGallery
{	
	public partial class SettingsMiniGallery : ModuleSettingsBase
	{
		/// <summary>
		/// Handles the loading of the module setting for this control
		/// </summary>
		public override void LoadSettings ()
		{
			try
			{
				if (!IsPostBack)
				{
					var settings = new MiniGallerySettings (this);

					// fill columns and rows lists
					ddlRows.Items.Add (new ListItem (
						Localization.GetString ("Auto.Text", LocalResourceFile), 
						Null.NullInteger.ToString ()));

					ddlColumns.Items.Add (new ListItem (
						Localization.GetString ("Auto.Text", LocalResourceFile), 
						Null.NullInteger.ToString ()));

					for (var i = 1; i <= 50; i++)
					{
						ddlRows.Items.Add (new ListItem (i.ToString ()));
						ddlColumns.Items.Add (new ListItem (i.ToString ()));
					}

					Utils.SelectByValue (ddlColumns, settings.Columns, 0); 
					// row number value have meaning only if columns number is set
					if (settings.Columns != Null.NullInteger)
						Utils.SelectByValue (ddlRows, settings.Rows, 0); 

					// Localize ();	
                	
					textImageWidth.Text = settings.ImageWidth;
					textImageHeight.Text = settings.ImageHeight;

					//textViewerCssClass.Text = settings.ViewerCssClass;
					textStyleSet.Text = settings.StyleSet;

					textMaxHeight.Text = 
						(settings.MaxHeight >= 0) ? settings.MaxHeight.ToString () : string.Empty;

					// 0 = none, 1 = other
					Utils.SelectByValue (ddlTarget, settings.Target, 1);
					if (ddlTarget.SelectedIndex == 1)
						textTarget.Text = settings.Target;

					// TODO: realize 
					checkUseViewer.Checked = settings.UseViewer;
					checkUseScrollbar.Checked = settings.UseScrollbar;

					checkShowTitles.Checked = settings.ShowTitles;
					checkExpand.Checked = settings.Expand;

					// read text before  / text after
					var module = new ModuleController ().GetTabModule (TabModuleId);
					editorHeader.Text = module.Header;
					editorFooter.Text = module.Footer;
				}
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
      
		/// <summary>
		/// handles updating the module settings for this control
		/// </summary>
		public override void UpdateSettings ()
		{
			try
			{
				var settings = new MiniGallerySettings (this);

				// style
				settings.StyleSet = textStyleSet.Text;

				// max. height
				int maxHeight;
				settings.MaxHeight = int.TryParse (textMaxHeight.Text, out maxHeight) ? maxHeight : -1;

				// parse and store image size
				settings.ImageWidth = Utils.ParseToUnit (textImageWidth.Text, 1).ToString();
				settings.ImageHeight = Utils.ParseToUnit (textImageHeight.Text, 1).ToString();
/*
				try 
				{
					var width = Unit.Parse (textImageWidth.Text);
					if (width.Value <= 0)
						settings.ImageWidth = string.Empty;
					else
						settings.ImageWidth = width.ToString();
				}
				catch 
				{
					settings.ImageWidth = string.Empty;
				}*/

				// link target, 1 = other
				settings.Target = (ddlTarget.SelectedIndex != 1) ?
					 ddlTarget.SelectedValue : textTarget.Text;

				// columns & rows
				settings.Columns = int.Parse (ddlColumns.SelectedValue);
				settings.Rows = int.Parse (ddlRows.SelectedValue);

				// settings.UserScrollbar = checkUseScrollbar.Checked;
				// settings.UseViewer = checkUseViewer.Checked;

				settings.ShowTitles = checkShowTitles.Checked;
				settings.Expand = checkExpand.Checked;

				// store text before / text after
				var mctrl = new ModuleController ();
				var module = mctrl.GetTabModule (TabModuleId);
				module.Header = editorHeader.Text;
				module.Footer = editorFooter.Text;
				mctrl.UpdateModule (module);
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
	}
}

