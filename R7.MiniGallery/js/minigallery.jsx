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
            <span className="MG_Item">
                {this.renderEditLink()}
                <a href={this.props.navigateUrl} target={this.props.target} title={this.props.title} className="MG_Link" {...this.props.linkAttrs}>
                    <img src={this.props.thumbnailUrl} alt={this.props.alt} style={this.props.style} className={"MG_Image" + this.props.cssClass} />
                </a>
            </span>
        );
    }
}

class MiniGallery extends React.Component {
    render() {
        return (
            <div className="MG_List MG_Auto">
                {this.props.images.map((img) => <MiniGalleryImage
                    navigateUrl={img.navigateUrl}
                    thumbnailUrl={img.thumbnailUrl}
                    target={img.target}
                    alt={img.alt}
                    title={img.title}
                    cssClass={img.cssClass}
                    style={img.style}
                    editUrl={img.editUrl}
                    editIcon={this.props.editIcon}
                    isEditable={this.props.isEditable}
                    linkAttrs={this.props.linkAttrs}
                    />)}
            </div>
        );
    }
}
