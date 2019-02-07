//
//  MiniGalleryDataProvider.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017-2019 Roman M. Yagodin
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

using System.Linq;
using R7.Dnn.Extensions.Collections;
using R7.Dnn.Extensions.Data;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.Data
{
    public class MiniGalleryDataProvider: Dal2DataProvider
    {
        public int GetBaseSortIndex (int moduleId)
        {
            var images = GetObjects<ImageInfo> (moduleId);
            if (!images.IsNullOrEmpty ()) {
                return images.Max (i => i.SortIndex);
            }

            return 0;
        }
    }
}
