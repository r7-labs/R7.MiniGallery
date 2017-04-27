class BlueimpQueryLightbox extends React.Component {
    render() {
        return (
            <div>
                <div id={this.props.gallery} className={"blueimp-gallery" + ((this.props.showControls === true)? " blueimp-gallery-controls" : "")}>
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
                    gallery={this.props.gallery}
                />
            </div>
        );
    }
}

(function ($, window, document) {
    $(() => {
        $(".minigallery-inner").each ((i, m) => {
            var moduleId = $(m).data ("module-id");
            ReactDOM.render (
                <BlueimpQueryLightbox
                    moduleId={moduleId}
                    isEditable={$(m).data ("is-editable")}
                    editIcon={$(m).data ("edit-icon")}
                    images={$(m).data ("images")}
                    showControls={true}
                    gallery={"gallery-" + moduleId}
                />, m
            );
        });
    });
}) (jQuery, window, document);