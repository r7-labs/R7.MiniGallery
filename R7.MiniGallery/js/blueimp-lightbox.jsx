class BlueimpLightbox extends React.Component {
    render() {
        return (
            <div>
                <div className={"blueimp-gallery" + ((this.props.showControls === true)? " blueimp-gallery-controls" : "")}>
                    <div className="slides"></div>
                    <h3 className="title"></h3>
                    <a className="prev">&lt;</a>
                    <a className="next">&gt;</a>
                    <a className="close">&times;</a>
                    <a className="play-pause"></a>
                    <ol className="indicator"></ol>
                </div>
                <MiniGallery
                    moduleId={this.props.moduleId}
                    isEditable={this.props.isEditable}
                    editIcon={this.props.editIcon}
                    images={this.props.images}
                />
            </div>
        );
    }
}

(function ($, window, document) {
    $(() => {
        $(".minigallery-root").each ((i, m) => {
            var moduleId = $(m).data ("module-id");
            ReactDOM.render (
                <BlueimpLightbox
                    moduleId={moduleId}
                    isEditable={$(m).data ("is-editable")}
                    editIcon={$(m).data ("edit-icon")}
                    images={$(m).data ("images")}
                    showControls={true}
                />, m
            );

            // TODO: Extract function, bind onclick statically
            $("a.MG_Link").click ((event) => {
                var target = event.target || event.srcElement;
                var root = $(target).closest(".minigallery-root");
                var link = target.src ? target.parentNode : target;
                var options = {
                    hidePageScrollbars: false,
                    index: link,
                    event: event,
                    container: root.find (".blueimp-gallery").get(0)
                };
                var links = root.find("a.MG_Link").get();
                blueimp.Gallery(links, options);
            });
        });
    });
}) (jQuery, window, document);