(function ($, window, document) {
    function getLightboxes () {
        const lightboxes = [];
        lightboxes ["Nonebox"] = MiniGallery;
        lightboxes ["Lightbox"] = MiniGallery;
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
            console.log (lightboxes);
            console.log (Lightbox);
            ReactDOM.render (
                <Lightbox
                    moduleId={moduleId}
                    isEditable={$(m).data ("is-editable")}
                    editIcon={$(m).data ("edit-icon")}
                    images={$(m).data ("images")}
                    showControls={true}
                    linkAttrs={$(m).data ("link-attrs")}
                />, m
            );
        });
    });
}) (jQuery, window, document);