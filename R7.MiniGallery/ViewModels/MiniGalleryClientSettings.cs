//
//  MiniGalleryClientSettings.cs
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

using R7.MiniGallery.Models;

namespace R7.MiniGallery.ViewModels
{
    public class MiniGalleryClientSettings
    {
        public string Target { get; protected set; }

        public string StyleSet { get; protected set; }

        public bool ShowTitles { get; protected set; }

        public bool EnableMoreImages { get; protected set; }

        public MiniGalleryClientSettings (MiniGallerySettings settings)
        {
            Target = settings.Target;
            StyleSet = settings.StyleSet;
            ShowTitles = settings.ShowTitles;
            EnableMoreImages = settings.EnableMoreImages;
        }
    }
}
