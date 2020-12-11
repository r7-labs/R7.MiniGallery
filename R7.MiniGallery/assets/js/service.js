var minigallery = minigallery || {};

minigallery.service = function ($, moduleId) {
    var baseServicePath = $.dnnSF (moduleId).getServiceRoot ("R7.MiniGallery");
    this.ajaxCall = function (type, controller, action, id, data, success, fail) {
        $.ajax ({
            type: type,
            url: baseServicePath + controller + "/" + action + (id != null ? "/" + id : ""),
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
    };
    
    this.getSettings = function (success, fail) {
        this.ajaxCall ("GET", "QuickSettings", "Get", null, null, success, fail);
    };
    
    this.updateSettings = function (success, fail, data) {
        this.ajaxCall ("POST", "QuickSettings", "Update", null, data, success, fail);
    };
}