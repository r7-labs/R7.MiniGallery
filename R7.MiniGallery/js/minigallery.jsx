class MiniGalleryImage extends React.Component {
    render() {
        return (
            <span className="MG_Item">
                <a href={this.props.EditUrl}>[edit]</a>
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
            <div className="links MG_List">
                {this.props.Images.map((img) => <MiniGalleryImage Src={img.ImageSrc} Alt={img.Alt} Title={img.Title} EditUrl={img.EditUrl} />)}
            </div>
        );
    }
}