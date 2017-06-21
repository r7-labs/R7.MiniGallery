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

using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Jint;
using JavaScriptEngineSwitcher.Jurassic;
using Newtonsoft.Json.Serialization;
using React;

namespace R7.MiniGallery.React
{
    public static class DnnReact
    {
        static readonly object reactSyncRoot = new object ();

        static bool _configured;

        static void Configure ()
        {
            var engineSwither = JsEngineSwitcher.Instance;

            // WTF: Factory should be added first to use it?
            engineSwither.EngineFactories.Clear ();
            engineSwither.EngineFactories.AddJurassic ();
            engineSwither.EngineFactories.AddJint ();

            var reactConfig = ReactSiteConfiguration.Configuration;
            reactConfig.SetLoadBabel (false);
            reactConfig.JsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
            reactConfig.ReuseJavaScriptEngines = true;

            #if DEBUG

            reactConfig.SetStartEngines (1);

            #endif
        }

        public static void ConfigureOnce ()
        {
            if (!_configured) {
                lock (reactSyncRoot) {
                    if (!_configured) {
                        Configure ();
                        _configured = true;
                    }
                }
            }
        }

        public static void AddScriptWithoutTransform (string fileName)
        {
            lock (reactSyncRoot) {
                ReactSiteConfiguration.Configuration.AddScriptWithoutTransform (fileName);
            }
        }
    }
}
