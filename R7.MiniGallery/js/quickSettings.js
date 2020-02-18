// TODO: Convert to React
var minigallery = minigallery || {};

minigallery.quickSettings = function (root, moduleId) {
	
	var updateSettings = function () {
		var deferred = $.Deferred();
        var service = new minigallery.service ($, moduleId);
        service.updateSettings (
            function (data) {
                // TODO: Update main view w/o page reload
                deferred.resolve ();
                document.location.reload (true);
            },
            function (xhr, status) {
                // TODO: error state
                deferred.reject ();
                console.log (xhr);
                console.log (status);
            },
            {
                imageCssClass: $("#mg_qs_imageCssClass_" + moduleId).val (),
                numberOfRecords: $("#mg_qs_numberOfRecords_" + moduleId).val ()
            }
        );
        
        return deferred.promise ();
    };
    
    var loadSettings = function () {
        var service = new minigallery.service ($, moduleId);
        var settings = service.getSettings (
            function (data) {
                // fill the form
                $("#mg_qs_imageCssClass_" + moduleId).val (data.imageCssClass);
                $("#mg_qs_numberOfRecords_" + moduleId).val (data.numberOfRecords);
            },
            function (xhr, status) {
                console.log (xhr);
                console.log (status);
                // TODO: error state
            }
        );
    };
    
    var cancelSettings = function () {
        var deferred = $.Deferred ();
        deferred.resolve ();
        return deferred.promise ();
    };
     
    var init = function () {
        // wire up the default save and cancel buttons
        $(root).dnnQuickSettings({
            moduleId: moduleId,
            onSave: updateSettings,
            onCancel: cancelSettings
        });
        loadSettings();
    }

    return {
        init: init
    }
};