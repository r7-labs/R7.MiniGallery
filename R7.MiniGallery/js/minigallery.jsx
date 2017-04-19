class MiniGalleryImage extends React.Component {
    render() {
        return <li><img src={this.props.Src} alt={this.props.Alt} title={this.props.Title} /></li>;
    }
}

class MiniGallery extends React.Component {
    render() {
        return (
            <ul className="list-inline">
                {this.props.Images.map((img) => <MiniGalleryImage Src={img.ImageSrc} Alt={img.Alt} Title={img.Title} />)}
            </ul>
        );
    }
}