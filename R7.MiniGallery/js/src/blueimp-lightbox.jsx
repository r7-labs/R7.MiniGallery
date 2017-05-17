class BlueimpLightbox extends React.Component {
    render() {
        return (
            <div>
                <div id={"gallery-" + this.props.moduleId} className={"blueimp-gallery" + ((this.props.showControls === true)? " blueimp-gallery-controls" : "")}>
                    <div className="slides"></div>
                    <h3 className="title"></h3>
                    <a className="prev">&#8249;</a>
                    <a className="next">&#8250;</a>
                    <a className="close">&times;</a>
                    <a className="play-pause"></a>
                    <ol className="indicator"></ol>
                </div>
                <MiniGallery
                    moduleId={this.props.moduleId}
                    isEditable={this.props.isEditable}
                    editIcon={this.props.editIcon}
                    images={this.props.images}
                    totalImages={this.props.totalImages}
                    settings={this.props.settings}
                    resources={this.props.resources}
                    lightboxType={this.props.lightboxType}
                    service={this.props.service}
                />
            </div>
        );
    }
}