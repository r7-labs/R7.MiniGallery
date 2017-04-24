class MiniGalleryImage extends React.Component {
    render() {
        return (
            <a href={this.props.Src} target="_blank" title={this.props.Title}>
                <img src={this.props.Src} alt={this.props.Alt} />
            </a>
        );
    }
}

class MiniGallery extends React.Component {
    render() {
        return (
            <div className="links">
                {this.props.Images.map((img) => <MiniGalleryImage Src={img.ImageSrc} Alt={img.Alt} Title={img.Title} />)}
            </div>
        );
    }
}