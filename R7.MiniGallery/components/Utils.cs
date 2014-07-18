using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.UI.Modules;
using DotNetNuke.Common;

namespace R7.MiniGallery
{
	public enum ImageViewer { YoxView, LightBox2, FancyBox2, ColorBox }

	// public enum ScrollBar { ? }
	public enum SortType { Index = 0, DateCreated, DateModified }
	/// <summary>
	/// Message severities
	/// </summary>
	public enum MessageSeverity { Info, Warning, Error };

	public class Utils 
	{
		/// <summary>
		/// Determines if the specified file is an images.
		/// </summary>
		/// <returns></returns>
		/// <param name="fileName">File name.</param>
		public static bool IsImage (string fileName)
		{
			if (!string.IsNullOrWhiteSpace(fileName))
				return Globals.glbImageFileTypes.Contains(
					Path.GetExtension(fileName).Substring(1).ToLowerInvariant());
			else
				return false;
		}

		/// <summary>
		/// Calculates longest common substring of s1 and s2.
		/// </summary>
		/// <param name="s1">first string.</param>
		/// <param name="s2">second string.</param>
		public static string LCS ( string s1, string s2 )
		{
			var a = new int [s1.Length + 1, s2.Length + 1];
			int u = 0,  v = 0;
			
			for (var i = 0; i < s1.Length; i++) 
				for (var j = 0; j < s2.Length; j++) 
					if (s1[i] == s2[j])
				{
					a[i+1,j+1] = a[i, j]+1;
					if (a[i+1,j+1] > a[u,v])
					{
						u = i+1;
						v = j+1;
					}
				}
			
			return s1.Substring(u - a[u,v], a[u,v]);
		}

		/// <summary>
		/// Formats the URL by DNN rules.
		/// </summary>
		/// <returns>Formatted URL.</returns>
		/// <param name="module">A module reference.</param>
		/// <param name="link">A link value. May be TabID, FileID=something or in other valid forms.</param>
		/// <param name="trackClicks">If set to <c>true</c> then track clicks.</param>
		public static string FormatURL (IModuleControl module, string link, bool trackClicks)
		{
			return DotNetNuke.Common.Globals.LinkClick 
				(link, module.ModuleContext.TabId, module.ModuleContext.ModuleId, trackClicks);
		}

		/// <summary>
		/// Formats the Edit control URL by DNN rules (popups supported).
		/// </summary>
		/// <returns>Formatted Edit control URL.</returns>
		/// <param name="module">A module reference.</param>
		/// <param name="controlKey">Edit control key.</param>
		/// <param name="args">Additional parameters.</param>
		public static string EditUrl (IModuleControl module, string controlKey, params string [] args)
		{
			var mctx = module.ModuleContext;
			var argList = new List<string>(args); 
			argList.Add ("mid");
			argList.Add (mctx.ModuleId.ToString());

			return mctx.NavigateUrl (mctx.TabId, controlKey, false, argList.ToArray());
		}

		public static int TryParseInt32 (string _value, int defValue)
		{
			int tmp;
			return int.TryParse (_value, out tmp)? tmp : defValue; 
		}

		/// <summary>
		/// Finds the item index by it's value in ListControl-type list.
		/// </summary>
		/// <returns>Item index.</returns>
		/// <param name="list">List control.</param>
		/// <param name="value">A value.</param>
		/// <param name="defaultIndex">Default index (in case item not found).</param>
		public static int FindIndexByValue (ListControl list, object value, int defaultIndex)
		{ 
			var index = 0;
			var strvalue = value.ToString();
			foreach (ListItem item in list.Items)
			{
				if (item.Value == strvalue) return index;
				index++;
			}
			return defaultIndex; 
		}

		/// <summary>
		/// Sets the selected index of ListControl-type list.
		/// </summary>
		/// <param name="list">List control.</param>
		/// <param name="value">A value.</param>
		/// <param name="defaultIndex">Default index (in case item not found).</param>
		public static void SetIndexByValue (ListControl list, object value, int defaultIndex)
		{
			list.SelectedIndex = FindIndexByValue(list, value, defaultIndex);
		}



		/// <summary>
		/// Display a message at the top of the specified module.
		/// </summary>
		/// <param name="module">Module reference.</param>
		/// <param name="severity">Message severity level.</param>
		/// <param name="message">Message text.</param>
		public static void Message (IModuleControl module, MessageSeverity severity, string message)
		{
			var label = new Label();
			label.CssClass = "dnnFormMessage dnnForm" + severity;
			label.Text = message;

			module.Control.Controls.AddAt(0, label);
		}

		public static Unit ParseToUnit (string value, double minvalue)
		{
			try 
			{
				var unit = Unit.Parse(value);
				if (unit.Value <= minvalue) 
					return Unit.Empty;
				return unit; 
			}
			catch
			{
				return Unit.Empty;
			}
		}


	}





}