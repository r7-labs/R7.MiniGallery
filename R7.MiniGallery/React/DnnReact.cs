//
//  ReactConfig.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using React;

namespace R7.MiniGallery.React
{
    public static class DnnReact
    {
        static readonly object reactSyncRoot = new object ();

        static bool _configured;

        static void Configure (IReactSiteConfiguration reactConfig)
        {
            reactConfig.LoadBabel = false;
        }

        public static void AddScriptWithoutTransform (string fileName)
        {
            lock (reactSyncRoot) {
                var reactConfig = ReactSiteConfiguration.Configuration;

                if (!_configured) {
                    Configure (reactConfig);
                    _configured = true;
                }

                reactConfig.AddScriptWithoutTransform (fileName);
            }
        }
    }
}
