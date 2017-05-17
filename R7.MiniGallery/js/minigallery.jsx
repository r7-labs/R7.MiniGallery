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
        return null;
    }

    renderTitle() {
        if (this.props.showTitle && !!this.props.title) {
            return (
                <p className="MG_Title">{this.props.title}</p>
            );
        }
        return null;
    }

    render() {
        return (
            <li className={(this.props.index % 2 === 0)? "MG_Item" : "MG_AltItem"} style={this.props.itemStyle}>
                {this.renderEditLink()}
                <a href={this.props.navigateUrl} target={this.props.target} title={this.props.title} className="MG_Link" {...this.props.linkAttrs}>
                    <img src={this.props.thumbnailUrl} alt={this.props.alt} style={this.props.style} className={(this.props.cssClass + " MG_Image").trimLeft()} />
                </a>
                {this.renderTitle()}
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

    getAllImages (e) {
        e.preventDefault ();

        this.setState ({
            loading: true,
            error: false,
            images: this.state.images
        });

        this.props.service.getAllImages (
            (data) => {
                if (data.length > 0) {
                    this.setState ({
                        loading: false,
                        error: false,
                        images: data
                    });

                    // TODO: Refactor this
                    if (this.props.lightboxType === "Colorbox") {
                        mgColorboxInit (jQuery);
                    }
                }
                else {
                    this.setErrorState ();
                }
            },
            (xhr, status) => {
                console.log (xhr);
                console.log (status);
                this.setErrorState ();
            }
        );
    }

    setErrorState () {
        this.setState ({
            loading: false,
            error: true,
            images: this.state.images
        });
    }

    renderMoreButton () {
        if (!this.props.settings.disableMoreImages && (this.props.totalImages > this.state.images.length)) {
            if (!this.state.loading && !this.state.error) {
                return (
                    <button className="btn btn-sm btn-block btn-default" onClick={this.getAllImages.bind(this)}>
                    {this.props.resources.moreImagesFormat.replace ("{0}", this.props.totalImages - this.state.images.length)}
                    </button>
                );
            }
            else if (this.state.loading) {
                return (
                    <div className="MG_MoreLoading">
                        <img src="/images/loading.gif" />
                    </div>
                );
            }
        }
        return null;
    }

    renderError () {
        if (this.state.error) {
            return (
                <div className="alert alert-danger" role="alert">
                    <strong>{this.props.resources.moreImagesErrorTitle}</strong> {this.props.resources.moreImagesErrorMessage}
                </div>
            );
        }
        return null;
    }

    render() {
        return (
            <div>
                <ul className={"MG_List MG_" + this.props.settings.styleSet}>
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
                        editIcon={this.props.editIcon}
                        isEditable={this.props.isEditable}
                        showTitle={this.props.settings.showTitles}
                        target={this.props.settings.target}
                        />)}
                </ul>
                {this.renderError()}
                {this.renderMoreButton()}
            </div>
        );
    }
}