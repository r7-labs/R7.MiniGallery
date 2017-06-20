window.MiniGalleryService = function ($, moduleId) {
    const baseServicepath = $.dnnSF (moduleId).getServiceRoot ("R7.MiniGallery");
    this.ajaxCall = function (type, controller, action, id, data, success, fail) {
        $.ajax ({
            type: type,
            url: baseServicepath + controller + "/" + action + (id != null ? "/" + id : ""),
            beforeSend: $.dnnSF (moduleId).setModuleHeaders,
            data: data
        }).done (function (retData) {
            if (success != undefined) {
                success (retData);
            }
        }).fail (function (xhr, status) {
            if (fail != undefined) {
                fail (xhr, status);
            }
        });
    }

    this.getAllImages = function (success, fail) {
        this.ajaxCall ("GET", "Image", "GetAll", null, null, success, fail);
    }
}