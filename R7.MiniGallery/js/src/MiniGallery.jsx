//
//  MiniGallery.jsx
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017-2020 Roman M. Yagodin
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

// TODO: Move to separate file
class MiniGalleryImage extends React.Component {
    renderEditLink () {
        if (this.props.isEditable) {
            return (
                <div className="r7-mg-edit">
	                <a href={this.props.editUrl}>
	                    <img src={this.props.editIcon} />
	                </a>
                </div>
            );
        }
        return null;
    }

    renderTitle() {
        if (this.props.showTitle && !!this.props.title) {
            return (
                <div className="r7-mg-title">{this.props.title}</div>
            );
        }
        return null;
    }
    
    getItemCssClass (index, isHidden) {
        var cssClass = (index % 2 === 0)? "r7-mg-item" : "r7-mg-alt-item";
        if (isHidden) {
            cssClass += " r7-mg-d-none";
        }
        return cssClass; 
    }
    
    render() {
        return (
            // TODO: Image CSS class from settings should override r7-mg-image
            <li className={this.getItemCssClass(this.props.index, this.props.isHidden)} style={this.props.itemStyle}>
                {this.renderEditLink()}
                <a href={this.props.navigateUrl} target={this.props.target} title={this.props.title} className="r7-mg-link" {...this.props.linkAttrs}>
                    <img src={this.props.thumbnailUrl} alt={this.props.alt} style={this.props.style} className={(this.props.cssClass + " r7-mg-image").trim()} />
                    {this.renderTitle()}
                </a>
            </li>
        );
    }
}

class MiniGallery extends React.Component {
    constructor (props) {
        super (props);
        this.state = {
            loading: false,
            images: this.props.images
        };
    }
    
    showAllImages (e) {
        e.preventDefault ();
        
        this.setState ({
            loading: true,
            error: false,
            images: this.state.images
        });
        
        this.state.images.map ((img) => {
            if (img.isHidden === true)
                img.isHidden = false;
        });
        
        this.setState ({
            loading: false,
            error: false,
            images: this.state.images
        });
    }
    
    getNumberOfHiddenImages (images) {
        var n = 0;
        images.map ((img) => {
            if (img.isHidden) n++; 
        });
        return n;
    }
    
    renderMoreButton () {
        // TODO: Remove loading and error states?
        const numOfHiddenImages = this.getNumberOfHiddenImages(this.state.images);
        if (this.props.settings.enableMoreImages && numOfHiddenImages > 0) {
            if (!this.state.loading && !this.state.error) {
                return (
                    <button className="btn btn-sm btn-block btn-outline-secondary" onClick={this.showAllImages.bind(this)}>
                    {this.props.resources.moreImagesFormat.replace ("{0}", numOfHiddenImages)}
                    </button>
                );
            }
            else if (this.state.loading) {
                return (
                    <div className="r7-mg-more-loading">
                        <img src="/images/loading.gif" />
                    </div>
                );
            }
        }
        return null;
    }
    
    renderBlueimp() {
        if (this.props.lightboxType === "BlueimpLightbox") {
            return (
                <div id={"gallery-" + this.props.moduleId} className={"blueimp-gallery" + ((this.props.showControls === true)? " blueimp-gallery-controls" : "")}>
                    <div className="slides"></div>
                    <h3 className="title"></h3>
                    <a className="prev">&#8249;</a>
                    <a className="next">&#8250;</a>
                    <a className="close">&times;</a>
                    <a className="play-pause"></a>
                    <ol className="indicator"></ol>
                </div>
            );
        }
        return null;
    }

    render() {
        return (
            <div>
                {this.renderBlueimp()}
                <ul className={"r7-mg-list r7-mg-" + this.props.settings.styleSet.toLowerCase()}>
                    {this.state.images.map((img, index) => <MiniGalleryImage
                        index={index}
                        navigateUrl={img.navigateUrl}
                        thumbnailUrl={img.thumbnailUrl}
                        alt={img.alt}
                        title={img.title}
                        cssClass={img.cssClass}
                        style={img.style}
                        editUrl={img.editUrl}
                        linkAttrs={img.linkAttributes}
                        itemStyle={img.itemStyle}
                        isHidden={img.isHidden}
                        editIcon={this.props.editIcon}
                        isEditable={this.props.isEditable}
                        showTitle={this.props.settings.showTitles}
                        target={this.props.settings.target}
                        />)}
                </ul>
                {this.renderMoreButton()}
            </div>
        );
    }
}