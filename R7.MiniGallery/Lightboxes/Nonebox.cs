//
// Nonebox.cs
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

using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Web.Client.ClientResourceManagement;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.Lightboxes
{
    public class Nonebox : LightboxBase, ILightbox
	{
		public Nonebox (): base (LightboxType.None, string.Empty)
		{
		}
		
		public override void Register (DnnJsInclude includeJs, DnnCssInclude includeCss, Literal literalScript)
		{
			// HACK: point to already defined stylesheet and dummy script
			includeJs.FilePath = "~/DesktopModules/R7.MiniGallery/R7.MiniGallery/js/dummy.js"; 
			includeCss.FilePath = "~/DesktopModules/R7.MiniGallery/R7.MiniGallery/module.css";

			// hide startup script block
			literalScript.Visible = false;
		}

		public override void ApplyTo (Image image, HyperLink link)
		{
		}

        #region ILightbox implementation

        public void Register (Page page)
        {
        }

        public string GetLinkAttributes (IImage image, int moduleId)
        {
            return string.Empty;
        }

        #endregion
    }
}
