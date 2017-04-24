//
//  ImageViewModel.cs
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
using DotNetNuke.Common;
using DotNetNuke.UI.Modules;
using R7.MiniGallery.Models;

namespace R7.MiniGallery.ViewModels
{
    public class ImageViewModel: IImage
    {
        protected IImage Model;

        protected ModuleInstanceContext ModuleContext;

        public ImageViewModel (IImage model, ModuleInstanceContext moduleContext)
        {
            Model = model;
            ModuleContext = moduleContext;
        }

        #region IImage implementation

        public string Alt {
            get { return Model.Alt; }
            set { throw new NotImplementedException (); }
        }

        public int CreatedByUserID {
            get { return Model.CreatedByUserID; }
            set { throw new NotImplementedException (); }
        }

        public DateTime CreatedOnDate {
            get { return Model.CreatedOnDate; }
            set { throw new NotImplementedException (); }
        }

        public int LastModifiedByUserID {
            get { return Model.LastModifiedByUserID; }
            set { throw new NotImplementedException (); }
        }

        public DateTime LastModifiedOnDate {
            get { return Model.LastModifiedOnDate; }
            set { throw new NotImplementedException (); }
        }

        public int ImageID {
            get { return Model.ImageID; }
            set { throw new NotImplementedException (); }
        }

        public int ImageFileID {
            get { return Model.ImageFileID; }
            set { throw new NotImplementedException (); }
        }

        public bool IsPublished {
            get { return Model.IsPublished; }
            set { throw new NotImplementedException (); }
        }

        public int ModuleID {
            get { return Model.ModuleID; }
            set { throw new NotImplementedException (); }
        }

        public int SortIndex {
            get { return Model.SortIndex; }
            set { throw new NotImplementedException (); }
        }

        public string Title {
            get { return Model.Title; }
            set { throw new NotImplementedException (); }
        }

        public string Url {
            get { return Model.Url; }
            set { throw new NotImplementedException (); }
        }

        #endregion

        public string ImageSrc {
            get {
                return Globals.LinkClick ("FileID=" + Model.ImageFileID, ModuleContext.TabId, ModuleContext.ModuleId, false);
            }
        }

        public string EditUrl {
            get {
                return ModuleContext.EditUrl ("imageid", Model.ImageID.ToString (), "Edit");
            }
        }
    }
}
