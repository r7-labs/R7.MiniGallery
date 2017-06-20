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

using System.Collections.Generic;
using Orc.SuperchargedReact.Core;
using Orc.SuperchargedReact.Web;

namespace R7.MiniGallery.React
{
    public static class DnnReact
    {
        static readonly object reactSyncRoot = new object ();

        static readonly Dictionary<string, ReactRunner> _runners = new Dictionary<string, ReactRunner> ();

        public static void AddScript (string alias, string fileName, ReactConfiguration config)
        {
            lock (reactSyncRoot) {
                _runners.Add (
                    alias,
                    new ReactRunner (fileName, config.EnableFileWatcher, config.EnableCompilation, config.DisableGlobalMembers, config.SerializerSettings)
                );
            }
        }
    }
}
