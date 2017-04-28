﻿//
//  LightboxFactory.cs
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

namespace R7.MiniGallery.Lightboxes
{
    public static class LightboxFactory
    {
        public static ILightbox Create (LightboxType lightboxType)
        {
            switch (lightboxType) {
                case LightboxType.BlueimpGallery:
                case LightboxType.Default:
                    return new BlueimpLightbox ();

               default:
                    return new Nonebox ();
            }
        }
    }
}
