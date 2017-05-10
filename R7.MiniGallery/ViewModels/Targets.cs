//
//  Targets.cs
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
using System.Web.Mvc;
using DotNetNuke.Services.Localization;

namespace R7.MiniGallery.ViewModels
{
    public static class Targets
    {
        public static IEnumerable<SelectListItem> GetTargets (string selectedTarget, string localResourceFile)
        {
            yield return new SelectListItem { Selected = (!string.IsNullOrEmpty (selectedTarget) &&
                                                          selectedTarget != "_blank" &&
                                                          selectedTarget != "_top" &&
                                                          selectedTarget != "_parent" &&
                                                          selectedTarget != "_self"),
                Text = Localization.GetString ("TargetCustom.Text", localResourceFile), Value = string.Empty };
            yield return new SelectListItem { Selected = string.IsNullOrEmpty (selectedTarget), Text = Localization.GetString ("TargetNone.Text", localResourceFile), Value = string.Empty};
            yield return new SelectListItem { Selected = selectedTarget == "_blank", Text = "_blank", Value = "_blank" };
            yield return new SelectListItem { Selected = selectedTarget == "_top", Text = "_top", Value = "_top" };
            yield return new SelectListItem { Selected = selectedTarget == "_parent", Text = "_parent", Value = "_parent" };
            yield return new SelectListItem { Selected = selectedTarget == "_self", Text = "_self", Value = "_self" };
        }
    }
}
