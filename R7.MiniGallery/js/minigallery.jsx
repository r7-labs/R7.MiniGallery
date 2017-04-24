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

class BlueimpGallery extends React.Component {
    render() {
        return (
            <div>
                <div id={"lightbox-" + this.props.ModuleId} className="blueimp-gallery blueimp-gallery-controls">
                    <div className="slides"></div>
                    <h3 className="title"></h3>
                    <a className="prev">&lt;</a>
                    <a className="next">&gt;</a>
                    <a className="close">&times;</a>
                    <a className="play-pause"></a>
                    <ol className="indicator"></ol>
                </div>
                <div className="links MG_List">
                    {this.props.Images.map((img) => <MiniGalleryImage Src={img.ImageSrc} Alt={img.Alt} Title={img.Title} EditUrl={img.EditUrl} />)}
                </div>
            </div>
        );
    }
}