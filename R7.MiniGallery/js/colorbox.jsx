function mgColorboxInit ($) {
    $("a[data-colorbox]").each ((i, link) => {
        $(link).colorbox ({
            rel: $(link).data ("colorbox"),
            photo: true,
            maxWidth: "95%",
            maxHeight: "95%"
        });
    });
}

(function ($) {
    $(() => {mgColorboxInit ($)})
}) (jQuery);