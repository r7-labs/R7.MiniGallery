//
//  minigallery.jsx
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

class MiniGalleryImage extends React.Component {
    renderEditLink () {
        if (this.props.isEditable) {
            return (
                <a href={this.props.editUrl}>
                    <img src={this.props.editIcon} className="MG_Edit" />
                </a>
            );
        }
        else {
            return (null);
        }
    }

    render() {
        return (
            <li className={(this.props.index % 2 === 0)? "MG_Item" : "MG_AltItem"} style={this.props.itemStyle}>
                {this.renderEditLink()}
                <a href={this.props.navigateUrl} target={this.props.target} title={this.props.title} className="MG_Link" {...this.props.linkAttrs}>
                    <img src={this.props.thumbnailUrl} alt={this.props.alt} style={this.props.style} className={(this.props.cssClass + " MG_Image").trimLeft()} />
                </a>
            </li>
        );
    }
}

class MiniGallery extends React.Component {
    render() {
        return (
            <ul className={"MG_List MG_" + this.props.styleSet}>
                {this.props.images.map((img, index) => <MiniGalleryImage
                    navigateUrl={img.navigateUrl}
                    thumbnailUrl={img.thumbnailUrl}
                    target={img.target}
                    alt={img.alt}
                    title={img.title}
                    cssClass={img.cssClass}
                    style={img.style}
                    editUrl={img.editUrl}
                    linkAttrs={img.linkAttributes}
                    editIcon={this.props.editIcon}
                    isEditable={this.props.isEditable}
                    itemStyle={img.itemStyle}
                    index={index}
                    />)}
            </ul>
        );
    }
}