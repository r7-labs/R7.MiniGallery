# R7.MiniGallery

[![BCH compliance](https://bettercodehub.com/edge/badge/roman-yagodin/R7.MiniGallery)](https://bettercodehub.com/)

Simple image gallery module for DNN Plaftorm with automatic thumbnails, blueimp Gallery, Lightbox2 and Colorbox support. 

# License

[![GPLv3](http://www.gnu.org/graphics/gplv3-127x51.png)](http://www.gnu.org/licenses/gpl.txt)

The *R7.MiniGallery* is free software: you can redistribute it and/or modify it under the terms of 
the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, 
or (at your option) any later version.

# Install

- Install *React*, *ReactDOM*, *jQuery-BlueimpGallery* packages from [R7.Dnn.JavaScriptLibraries](https://github.com/roman-yagodin/R7.Dnn.JavaScriptLibraries/releases)
- Install [R7.Dnn.Extensions](https://github.com/roman-yagodin/R7.Dnn.Extensions/releases) package
- Install [R7.MiniGallery](https://github.com/roman-yagodin/R7.MiniGallery/releases) package

### Automatic thumbnails

[R7.ImageHandler](https://github.com/roman-yagodin/R7.ImageHandler) should be installed first to allow automatic image thumbnails generation.

Otherwise, you can disable using of image handler in the module settings, and provide manually generated thumbnails, linking them to original big images. In this case, "Image Width / Height" settings could be used to scale thumbnails in the browser.

Standard DNN 8+ imagehandler support is planned.
