class MiniGalleryImage extends React.Component {
    renderEditLink () {
        if (this.props.IsEditable) {
            return (
                <a href={this.props.EditUrl}>
                    <img src={this.props.EditIcon} className="MG_Edit" />
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
                <a href={this.props.Src} target="_blank" title={this.props.Title} className="MG_Link">
                    <img src={this.props.Src} alt={this.props.Alt} className="MG_Image" />
                </a>
            </span>
        );
    }
}

class MiniGallery extends React.Component {
    render() {
        return (
            <div className="MG_List MG_Auto">
                {this.props.Images.map((img) => <MiniGalleryImage
                    Src={img.ImageSrc}
                    Alt={img.Alt}
                    Title={img.Title}
                    EditUrl={img.EditUrl}
                    EditIcon={this.props.EditIcon}
                    IsEditable={this.props.IsEditable}
                    />)}
            </div>
        );
    }
}
