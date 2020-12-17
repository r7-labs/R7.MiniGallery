//
//  IImage.cs
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

using System;

namespace R7.MiniGallery.Models
{
    public interface IImage
	{
        int ImageID { get; }

		int ModuleID { get; }

		int ImageFileID { get; }

		string Alt { get; }

		string Title { get; }

		string Url { get; }

        bool OpenInLightbox { get; }

        string CssClass { get; }

		int SortIndex { get; }

        DateTime? StartDate { get; }

        DateTime? EndDate { get; }

        int LastModifiedByUserID { get; }

        DateTime LastModifiedOnDate { get; }

        int CreatedByUserID { get; }

        DateTime CreatedOnDate { get; }
    }

    public interface IImageWritable: IImage
    {
        new int ImageID { get; set; }

        new int ModuleID { get; set; }

        new int ImageFileID { get; set; }

        new string Alt { get; set; }

        new string Title { get; set; }

        new string Url { get; set; }

        new bool OpenInLightbox { get; set; }

        new string CssClass { get; set; }

        new int SortIndex { get; set; }

        new DateTime? StartDate { get; set; }

        new DateTime? EndDate { get; set; }

        new int LastModifiedByUserID { get; set; }

        new DateTime LastModifiedOnDate { get; set; }

        new int CreatedByUserID { get; set; }

        new DateTime CreatedOnDate { get; set; }
    }
}

