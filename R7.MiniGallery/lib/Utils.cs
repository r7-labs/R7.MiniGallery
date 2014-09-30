//
// Utils.cs
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
using System.Web;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.UI.Modules;
using DotNetNuke.UI.Skins;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Localization;
using DotNetNuke.Web.UI.WebControls;

namespace R7.MiniGallery
{
	// public enum ScrollBar { ? }
	public enum SortType
	{
		Index = 0,
		DateCreated,
		DateModified

	}

	/// <summary>
	/// Module message types.
	/// </summary>
	public enum MessageType
	{
		// duplicate ModuleMessage.ModuleMessageType values here
		Success = DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.GreenSuccess,
		Info = DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.BlueInfo,
		Warning = DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning,
		Error = DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError
	}

	public class Utils
	{
		public static string GetUserDisplayName (int userId)
		{
			var portalId = PortalController.GetCurrentPortalSettings ().PortalId;
			var user = UserController.GetUserById (portalId, userId);

			// TODO: "System" user name needs localization
			return (user != null) ? user.DisplayName : "System";
		}

		/// <summary>
		/// Determines if the specified file is an images.
		/// </summary>
		/// <returns></returns>
		/// <param name="fileName">File name.</param>
		public static bool IsImage (string fileName)
		{
			if (!string.IsNullOrWhiteSpace (fileName))
				return Globals.glbImageFileTypes.Contains (
					Path.GetExtension (fileName).Substring (1).ToLowerInvariant ());
			else
				return false;
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
		public static string EditUrl (IModuleControl module, string controlKey, params string[] args)
		{
			var argList = new List<string> (args); 
			argList.Add ("mid");
			argList.Add (module.ModuleContext.ModuleId.ToString ());

			return module.ModuleContext.NavigateUrl (module.ModuleContext.TabId, controlKey, false, argList.ToArray ());
		}

		/// <summary>
		/// Finds the item index by it's value in ListControl-type list.
		/// </summary>
		/// <returns>Item index.</returns>
		/// <param name="list">List control.</param>
		/// <param name="value">A value.</param>
		/// <param name="defaultIndex">Default index (in case item not found).</param>
		public static int FindIndexByValue (ListControl list, object value, int defaultIndex = 0)
		{ 
			if (value != null)
			{
				var index = 0;
				var strvalue = value.ToString ();
				foreach (ListItem item in list.Items)
				{
					if (item.Value == strvalue)
						return index;
					index++;
				}
			}

			return defaultIndex; 
		}

		/// <summary>
		/// Sets the selected index of ListControl-type list.
		/// </summary>
		/// <param name="list">List control.</param>
		/// <param name="value">A value.</param>
		/// <param name="defaultIndex">Default index (in case item not found).</param>
		public static void SelectByValue (ListControl list, object value, int defaultIndex = 0)
		{
			list.SelectedIndex = FindIndexByValue (list, value, defaultIndex);
		}

		/// <summary>
		/// Displays a message of messageType for specified module with heading, with optional localization.
		/// </summary>
		/// <param name="module">Module.</param>
		/// <param name="heading">Message heading.</param>
		/// <param name="message">Message body.</param>
		/// <param name="messageType">Message type.</param>
		/// <param name="localize">If set to <c>true</c> localize message and heading.</param>
		public static void Message (PortalModuleBase module, string heading, string message, MessageType messageType = MessageType.Info, bool localize = false)
		{
			var locheading = localize ? Localization.GetString (heading, module.LocalResourceFile) : heading;
			var locmessage = localize ? Localization.GetString (message, module.LocalResourceFile) : message;
			Skin.AddModuleMessage (module, locheading, locmessage,
				(DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType)messageType);
		}

		/// <summary>
		/// Displays a message of messageType for specified module, with optional localization.
		/// </summary>
		/// <param name="module">Module.</param>
		/// <param name="message">Message body.</param>
		/// <param name="messageType">Message type.</param>
		/// <param name="localize">If set to <c>true</c> localize message.</param>
		public static void Message (PortalModuleBase module, string message, MessageType messageType = MessageType.Info, bool localize = false)
		{
			var locmessage = localize ? Localization.GetString (message, module.LocalResourceFile) : message;
			Skin.AddModuleMessage (module, locmessage,
				(DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType)messageType);
		}

		public static bool IsNull<T> (Nullable<T> n) where T: struct
		{
			// NOTE: n.HasValue is equvalent to n != null
			if (n.HasValue && !Null.IsNull (n.Value))
				return false;

			return true;
		}

		public static Nullable<T> ToNullable<T> (T n) where T: struct
		{
			return Null.IsNull (n) ? null : (Nullable<T>)n;
		}

		/// <summary>
		/// Parses specified string value to a nullable int, 
		/// also with convertion of Null.NullInteger to null 
		/// </summary>
		/// <returns>The nullable int.</returns>
		/// <param name="value">String value to parse.</param>
		public static int? ParseToNullableInt (string value)
		{
			// TODO: Make another variant of ParseToNullableInt() without using DNN Null object

			int n;

			if (int.TryParse (value, out n))
				return Null.IsNull (n) ? null : (int?)n;
			else
				return null;
		}

		/*
		public static Nullable<T> ParseToNullable<T>(string value) where T: struct
		{
			T n;

			if (Convert.ChangeType(value, typeof(T))
				return Null.IsNull (n)? null : (Nullable<T>) n;
			else
				return null;
		}*/

		/// <summary>
		/// Formats the list of arguments, excluding empty
		/// </summary>
		/// <returns>Formatted list.</returns>
		/// <param name="separator">Separator.</param>
		/// <param name="args">Arguments.</param>
		public static string FormatList (string separator, params object[] args)
		{
			var sb = new StringBuilder (args.Length);

			var i = 0;
			foreach (var a in args)
			{
				if (!string.IsNullOrWhiteSpace (a.ToString ()))
				{
					if (i++ > 0)
						sb.Append (separator);

					sb.Append (a);
				}
			}

			return sb.ToString ();
		}

		public static string FirstCharToUpper (string s)
		{
			if (!string.IsNullOrWhiteSpace (s))
			if (s.Length == 1)
				return s.ToUpper ();
			else
				return s.ToUpper () [0].ToString () + s.Substring (1);
			else
				return s;
		}

		public static string FirstCharToUpperInvariant (string s)
		{
			if (!string.IsNullOrWhiteSpace (s))
			if (s.Length == 1)
				return s.ToUpperInvariant ();
			else
				return s.ToUpperInvariant () [0].ToString () + s.Substring (1);
			else
				return s;
		}

		public static void SynchronizeModule (IModuleControl module)
		{
			ModuleController.SynchronizeModule (module.ModuleContext.ModuleId);

			// NOTE: update module cache (temporary fix before 7.2.0)?
			// more info: https://github.com/dnnsoftware/Dnn.Platform/pull/21
			var moduleController = new ModuleController ();
			moduleController.ClearCache (module.ModuleContext.TabId);

		}

		public static Unit ParseToUnit (string value, double minvalue)
		{
			try
			{
				var unit = Unit.Parse (value);
				if (unit.Value <= minvalue)
					return Unit.Empty;
				return unit; 
			}
			catch
			{
				return Unit.Empty;
			}
		}

		public static int TryParseInt32 (string _value, int defValue)
		{
			int tmp;
			return int.TryParse (_value, out tmp) ? tmp : defValue; 
		}

		public static void DnnFilePickerUploaderHack (DnnFilePickerUploader picker, PortalSettings portalSettings)
		{
			if (picker.FileID > 0)
				picker.FilePath = FileManager.Instance.GetUrl (
					FileManager.Instance.GetFile (picker.FileID))
					.Remove (0, portalSettings.HomeDirectory.Length);
		}
	}
	// class
}
// namespace
