class BlueimpLightbox extends React.Component {
    render() {
        return (
            <div className="blueimp-gallery blueimp-gallery-controls">
                <div className="slides"></div>
                <h3 className="title"></h3>
                <a className="prev">&lt;</a>
                <a className="next">&gt;</a>
                <a className="close">&times;</a>
                <a className="play-pause"></a>
                <ol className="indicator"></ol>
            </div>
        );
    }
}

(function ($, window, document) {
    $(() => {
        ReactDOM.render(
            <BlueimpLightbox />, $(".minigallery-lightbox").get(0)
        );
        var container = $(".minigallery-lightbox .blueimp-gallery").get(0);
        $(".minigallery-inner").each ((i, m) => {
            var moduleId = $(m).data ("module-id");
            ReactDOM.render(
                <MiniGallery
                    moduleId={moduleId}
                    isEditable={$(m).data ("is-editable")}
                    editIcon={$(m).data ("edit-icon")}
                    images={$(m).data ("images")}
                />, m
            );
            // TODO: Extract function, bind onclick statically
            $("a.MG_Link").click ((event) => {
                var target = event.target || event.srcElement,
                link = target.src ? target.parentNode : target,
                options = {
                    hidePageScrollbars: false,
                    index: link,
                    event: event,
                    container: container
                };
                var links = $(target).closest(".minigallery-inner").find("a.MG_Link").get();
                blueimp.Gallery(links, options);
            });
        });
    });
}) (jQuery, window, document);