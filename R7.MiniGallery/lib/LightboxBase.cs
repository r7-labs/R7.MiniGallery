//
// LightboxBase.cs
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
	public abstract class LightboxBase
	{
		protected string Key;
		
		public LightboxType LightboxType { get; set; }
	
		protected LightboxBase ()
		{
		}

		protected LightboxBase (LightboxType lightboxType, string key)
		{
			LightboxType = lightboxType;
			Key = key;
		}

		// NOTE: using ClientResourceManager.RegisterStyleSheet(), ClientResourceManager.RegisterScript() and
		// Page.ClientScript.RegisterStartupScript() methods won't produce cached content, 
		// so we use DnnJsInclude, DnnCssInclude controls to make links on the lighbox scripts and stylesheets, 
		// and literal to store startup script block. Produced content will be cached this way. 	

		public abstract void Register (DnnJsInclude includeJs, DnnCssInclude includeCss, Literal literalScript);
		
		public abstract void ApplyTo (Image image, HyperLink link);
	}
}

