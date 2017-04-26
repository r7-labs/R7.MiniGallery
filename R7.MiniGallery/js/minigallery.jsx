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
                <a href={this.props.NavigateUrl} target={this.props.Target} title={this.props.Title} className="MG_Link">
                    <img src={this.props.ThumbnailUrl} alt={this.props.Alt} style={this.props.Style} style={this.props.Style} className={"MG_Image" + this.props.CssClass} />
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
                    NavigateUrl={img.NavigateUrl}
                    ThumbnailUrl={img.ThumbnailUrl}
                    Target={img.Target}
                    Alt={img.Alt}
                    Title={img.Title}
                    CssClass={img.CssClass}
                    Style={img.Style}
                    EditUrl={img.EditUrl}
                    EditIcon={this.props.EditIcon}
                    IsEditable={this.props.IsEditable}
                    />)}
            </div>
        );
    }
}
