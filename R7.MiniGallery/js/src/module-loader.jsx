//
//  module-loader.jsx
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

(function ($, window, document) {
    $(() => {
        $(".minigallery-root").each ((i, m) => {
            const moduleId = $(m).data ("module-id");
            ReactDOM.render (
                <MiniGallery
                    moduleId={moduleId}
                    isEditable={$(m).data ("is-editable")}
                    editIcon={$(m).data ("edit-icon")}
                    images={$(m).data ("images")}
                    totalImages={$(m).data ("total-images")}
                    settings={$(m).data ("settings")}
                    resources={$(m).data ("resources")}
                    lightboxType={$(m).data ("lightboxType")}
                    showControls={true}
                    service={new window.MiniGalleryService ($, moduleId)}
                />, m
            );
        });
    });
}) (jQuery, window, document);
