(function ($, window, document) {
    function getLightboxes () {
        const lightboxes = [];
        lightboxes ["Nonebox"] = MiniGallery;
        lightboxes ["Lightbox"] = MiniGallery;
        lightboxes ["Colorbox"] = MiniGallery;
        if (typeof (BlueimpLightbox) !== "undefined") {
            lightboxes ["BlueimpLightbox"] = BlueimpLightbox;
        }
        return lightboxes;
    }

    $(() => {
        var lightboxes = getLightboxes ();
        $(".minigallery-root").each ((i, m) => {
            const moduleId = $(m).data ("module-id");
            const Lightbox = lightboxes [$(m).data ("lightbox-type")];
            ReactDOM.render (
                <Lightbox
                    moduleId={moduleId}
                    isEditable={$(m).data ("is-editable")}
                    editIcon={$(m).data ("edit-icon")}
                    styleSet={$(m).data ("style-set")}
                    images={$(m).data ("images")}
                    showControls={true}
                />, m
            );
        });
    });
}) (jQuery, window, document);