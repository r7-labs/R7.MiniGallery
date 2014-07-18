﻿//
// EmployeeModuleBase.cs
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
using DotNetNuke.Entities.Modules;

namespace R7.MiniGallery
{
	/// <summary>
	/// MiniGallery portal module base.
	/// </summary>
	public class MiniGalleryPortalModuleBase : PortalModuleBase
	{
		private MiniGalleryController ctrl = null;

		protected MiniGalleryController MiniGalleryController
		{
			get { return ctrl ?? (ctrl = new MiniGalleryController ()); }
		}

		private MiniGallerySettings settings = null;

		protected MiniGallerySettings MiniGallerySettings
		{
			get { return settings ?? (settings = new MiniGallerySettings (this)); }
		}

		public string DataCacheKey
		{
			get
			{
				return "TabModule:" + TabModuleId + ":" +
				System.Threading.Thread.CurrentThread.CurrentCulture;
			}
		}
	}

	/// <summary>
	/// MiniGallery module settings base.
	/// </summary>
	public class MiniGalleryModuleSettingsBase : ModuleSettingsBase
	{
		private MiniGalleryController ctrl = null;

		protected MiniGalleryController MiniGalleryController
		{
			get { return ctrl ?? (ctrl = new MiniGalleryController ()); }
		}

		private MiniGallerySettings settings = null;

		protected MiniGallerySettings MiniGallerySettings
		{
			get { return settings ?? (settings = new MiniGallerySettings (this)); }
		}

		public string DataCacheKey
		{
			get
			{
				return "TabModule:" + TabModuleId + ":" +
				System.Threading.Thread.CurrentThread.CurrentCulture;
			}
		}
	}
}
