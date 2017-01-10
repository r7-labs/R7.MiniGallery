//
// SettingsMiniGallery.ascx.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2017
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
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using R7.DotNetNuke.Extensions.ControlExtensions;
using R7.DotNetNuke.Extensions.Modules;
using R7.DotNetNuke.Extensions.ModuleExtensions;

namespace R7.MiniGallery
{
    public partial class SettingsMiniGallery : ModuleSettingsBase<MiniGallerySettings>
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			// fill columns combo
			comboColumns.AddItem (LocalizeString ("Auto.Text"), Null.NullInteger.ToString ());
			for (var i = 1; i <= 50; i++)
				comboColumns.AddItem (i.ToString (), i.ToString());

			// fill target combo
			comboTarget.AddItem (LocalizeString ("Custom.Text"), "@custom");
			comboTarget.AddItem (LocalizeString ("None.Text"), string.Empty);
			comboTarget.AddItem ("_blank", "_blank");
			comboTarget.AddItem ("_top", "_top");
			comboTarget.AddItem ("_parent", "_parent");
			comboTarget.AddItem ("_self", "_self");

			// fill lightbox type combo
			comboLightboxType.AddItem (LocalizeString ("None.Text"), LightboxType.None.ToString ());
			comboLightboxType.AddItem (LocalizeString ("Default.Text"), LightboxType.Default.ToString ());
			comboLightboxType.AddItem (LightboxType.LightBox.ToString (), LightboxType.LightBox.ToString ());
			comboLightboxType.AddItem (LightboxType.ColorBox.ToString (), LightboxType.ColorBox.ToString ());

			// fill style set combo
			comboStyleSet.AddItem (LocalizeString ("Custom.Text"), "@custom");
			comboStyleSet.AddItem ("Fixed", "Fixed");
			comboStyleSet.AddItem ("Auto", "Auto");
		}

		/// <summary>
		/// Handles the loading of the module setting for this control
		/// </summary>
		public override void LoadSettings ()
		{
			try
			{
				if (!IsPostBack)
				{
                    // columns
                    comboColumns.SelectByValue (Settings.Columns.ToString());
					checkExpand.Checked = Settings.ExpandColumns;

					comboLightboxType.SelectByValue (Settings.LightboxType.ToString());

					// thumb size
					if (!Null.IsNull (Settings.ThumbWidth))
						textThumbWidth.Text = Settings.ThumbWidth.ToString ();

					if (!Null.IsNull (Settings.ThumbHeight))
						textThumbHeight.Text = Settings.ThumbHeight.ToString ();

					// image size
					textImageWidth.Text = Settings.ImageWidth.ToString ();
					textImageHeight.Text = Settings.ImageHeight.ToString ();

					// number of records
					if (!Null.IsNull (Settings.NumberOfRecords))
						textNumberOfRecords.Text = Settings.NumberOfRecords.ToString ();

					// image handler
					checkUseImageHandler.Checked = Settings.UseImageHandler;
					textImageHandlerParams.Text = Settings.ImageHandlerParams;

					// max. height
					textMaxHeight.Text = Settings.MaxHeight.ToString();

					// style set
					var styleSetIndex = comboStyleSet.FindIndexByValue (Settings.StyleSet);
					if (styleSetIndex > 0)
					{
						comboStyleSet.Items [styleSetIndex].Selected = true;
					}
					else
					{
						comboStyleSet.SelectedIndex = 0; // custom
						textStyleSet.Text = Settings.StyleSet;
					}
						
					// link target
					var targetIndex = comboTarget.FindIndexByValue (Settings.Target);
					if (targetIndex > 0)
					{
						comboTarget.Items [targetIndex].Selected = true;
					}
					else
					{
						if (!string.IsNullOrWhiteSpace (Settings.Target))
						{
                        	comboTarget.SelectedIndex = 0; // custom
                            textTarget.Text = Settings.Target;
						}
						else
							comboTarget.SelectedIndex = 1; // none
					}

					// sort order (DESC sorting done if "-SortIndex" value)
					checkSortOrder.Checked = Settings.SortOrder == "SortIndex";

					// other
					checkUseScrollbar.Checked = Settings.UseScrollbar;
					checkShowTitles.Checked = Settings.ShowTitles;

					// read header and footer
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
		/// Handles updating the module settings for this control
		/// </summary>
		public override void UpdateSettings ()
		{
			try
			{
				// max. height
				Settings.MaxHeight = Unit.Parse (textMaxHeight.Text);

				// thumb size
				Settings.ThumbWidth = Utils.TryParseInt32 (textThumbWidth.Text, Null.NullInteger);
				Settings.ThumbHeight = Utils.TryParseInt32 (textThumbHeight.Text, Null.NullInteger);

				// number of records
				Settings.NumberOfRecords = Utils.TryParseInt32 (textNumberOfRecords.Text, Null.NullInteger);

				// image handler
				Settings.UseImageHandler = checkUseImageHandler.Checked;
				Settings.ImageHandlerParams = textImageHandlerParams.Text;

				// image size
				Settings.ImageWidth = Unit.Parse (textImageWidth.Text);
				Settings.ImageHeight = Unit.Parse (textImageHeight.Text);
			
				// style set, 0 = custom
				Settings.StyleSet = (comboStyleSet.SelectedIndex != 0) ?
					comboStyleSet.SelectedValue : textStyleSet.Text;

				// link target, 0 = custom
				Settings.Target = (comboTarget.SelectedIndex != 0) ?
					 comboTarget.SelectedValue : textTarget.Text;

				// columns
				Settings.Columns = int.Parse (comboColumns.SelectedValue);
				Settings.ExpandColumns = checkExpand.Checked;

				// lightbox type
				Settings.LightboxType = (LightboxType)Enum.Parse (typeof(LightboxType), comboLightboxType.SelectedValue, true);

				// sort order
				Settings.SortOrder = checkSortOrder.Checked? "SortIndex" : "-SortIndex";

				// other 
				Settings.UseScrollbar = checkUseScrollbar.Checked;
				Settings.ShowTitles = checkShowTitles.Checked;

				// replace header and footer
				if (checkReplaceHeaderAndFooter.Checked)
				{
					var mctrl = new ModuleController ();
					var module = mctrl.GetTabModule (TabModuleId);
					module.Header = editorHeader.Text;
					module.Footer = editorFooter.Text;
					mctrl.UpdateModule (module);
				}

                SettingsRepository.SaveSettings (ModuleConfiguration, Settings);

				Utils.SynchronizeModule (this);
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
	}
}

