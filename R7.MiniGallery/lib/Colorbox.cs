//
// Lightbox.cs
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
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Web.Client.ClientResourceManagement;

namespace R7.MiniGallery
{
	public class Colorbox : LightboxBase
	{
		public Colorbox (Page page, string key): base (LightboxType.ColorBox, page, key)
		{
		}
		
		public override void Register ()
		{
			ClientResourceManager.RegisterStyleSheet (page, "~/js/colorbox/example1/colorbox.css");
			ClientResourceManager.RegisterScript (page, "~/js/colorbox/jquery.colorbox-min.js");

			if (!page.ClientScript.IsStartupScriptRegistered (page.GetType (), "colorbox_" + key))
			{
				var scriptTemplate = "$(document).ready(function(){" +
					"$(\"a[data-colorbox=module_[KEY]]\")" +
					".colorbox({rel:\"module_[KEY]\",photo:true,maxWidth:\"95%\",maxHeight:\"95%\"});" +
			        "});";

				page.ClientScript.RegisterStartupScript (page.GetType (), 
					"colorbox_" + key, scriptTemplate.Replace ("[KEY]", key), true);
			}
		}

		public override void ApplyTo (Image image, HyperLink link)
		{
			// add attribute to use with selector
			link.Attributes.Add ("data-colorbox", "module_" + key);
			link.Attributes.Remove ("target");

			// Colorbox displays link title, not image title
			link.ToolTip = image.ToolTip;
			
			// HACK: Colorbox require link URL have file extension or photo=true to load image properly
			// link.NavigateUrl += "&ext=." + image.File.Extension;
		}
	}
}
