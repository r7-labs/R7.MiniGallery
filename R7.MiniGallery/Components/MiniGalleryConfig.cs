//
//  MiniGalleryConfig.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2019 Roman M. Yagodin
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

using R7.Dnn.Extensions.Configuration;
using R7.MiniGallery.Models;

namespace R7.University.Components
{
    public static class MiniGalleryConfig
    {
        static readonly ExtensionYamlConfig<MiniGalleryPortalConfig> _config;

        static MiniGalleryConfig ()
        {
            _config = new ExtensionYamlConfig<MiniGalleryPortalConfig> ("R7.MiniGallery.yml", null);
        }

        public static MiniGalleryPortalConfig GetInstance (int portalId) => _config.GetInstance (portalId);

        public static MiniGalleryPortalConfig Instance => _config.Instance;
    }
}
