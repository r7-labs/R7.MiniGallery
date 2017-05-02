//
// Lightbox.cs
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
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client.ClientResourceManagement;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.Lightboxes
{
    public class Lightbox: ILightbox
	{
        public void Register (Page page)
        {
            JavaScript.RequestRegistration ("Lightbox2");
            ClientResourceManager.RegisterScript (page, "~/DesktopModules/MVC/R7.MiniGallery/js/lightbox2.min.js");
            ClientResourceManager.RegisterStyleSheet (page, "~/Resources/Libraries/Lightbox2/02_09_00/css/lightbox.min.css");
        }

        public string GetLinkAttributes (IImage image, int moduleId)
        {
            return $"{{\"data-lightbox\":\"gallery-{moduleId}\",\"data-title\":\"{image.Title}\"}}";
        }
    }
}
