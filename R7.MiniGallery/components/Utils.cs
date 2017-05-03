//
// Utils.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2017 Roman M. Yagodin
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

using System.Text;
using System.Text.RegularExpressions;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.UI.Modules;

namespace R7.MiniGallery
{
    public class Utils
	{
        public static string FormatURL (IModuleControl module, string link, bool trackClicks)
        {
            return Globals.LinkClick
                (link, module.ModuleContext.TabId, module.ModuleContext.ModuleId, trackClicks);
        }

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

        public static int ExtractInt32 (string text, int defaultValue = default (int))
        {
            var matches = Regex.Matches (text, @"\d+");
            if (matches != null && matches.Count > 0)
            {
                int result;
                if (int.TryParse(matches[0].Value, out result))
                    return result;
            }

            return defaultValue;
        }
	}
}
