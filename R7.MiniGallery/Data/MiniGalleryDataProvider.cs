﻿//
//  MiniGalleryDataProvider.cs
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
using DotNetNuke.Common.Utilities;
using R7.DotNetNuke.Extensions.Data;

namespace R7.MiniGallery.Data
{
    public class MiniGalleryDataProvider: Dal2DataProvider
    {
        public IEnumerable<ImageInfo> GetImagesTopN (int moduleId, bool showAll, bool sortAscending, int topn = -1)
        {
            var sql = string.Format (
                          "SELECT {0} * FROM dbo.MiniGallery_Images WHERE ModuleId={1} {2} ORDER BY SortIndex {3};",
                          !Null.IsNull (topn) ? string.Format ("TOP({0})", topn) : string.Empty,
                          moduleId,
                          !showAll ? "AND IsPublished=1" : string.Empty,
                          sortAscending ? "ASC" : "DESC"
                      );

            return GetObjects<ImageInfo> (System.Data.CommandType.Text, sql);
        }

    }
}
