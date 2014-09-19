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
	public class Lightbox : LightboxBase
	{
		public Lightbox (string key): base (LightboxType.LightBox, key)
		{
		}
		
		public override void Register (DnnJsInclude includeJs, DnnCssInclude includeCss, Literal literalScript)
		{
			includeJs.FilePath = "~/Resources/Shared/components/lightbox/js/lightbox.min.js";
			includeCss.FilePath = "~/Resources/Shared/components/lightbox/css/lightbox.css";

			// no startup script required for the Lightbox
			literalScript.Visible = false;
		}

		public override void ApplyTo (Image image, HyperLink link)
		{
			link.Attributes.Add ("data-lightbox", "module_" + Key);
			link.Attributes.Remove ("target");
					
			if (!string.IsNullOrWhiteSpace (image.ToolTip))
				link.Attributes.Add ("data-title", image.ToolTip);
		}
	}
}
