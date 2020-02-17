var minigallery = minigallery || {};

minigallery.quickSettings = function(root, moduleId) {
	
	var saveSettings = function () {
        alert("Save Settings");
        var service = new minigallery.service ($, moduleId);
        // TODO: Get values from form
        // update settings on server
        service.updateSettings (
            function (data) {
                // TODO: Call setState
            },
            function (xhr, status) {
                console.log (xhr);
                console.log (status);
                // TODO: Call setErrorState
            }
        );
    };

    var cancelSettings = function () {
        // do nothing?
    };

    var loadSettings = function () {
        // TODO: Get values from server, fill the form
        var service = new minigallery.service ($, moduleId);
        var settings = service.getSettings (
            function (data) {
                if (data.length > 0) {
                    // TODO: fill the form
                }
                else {
                    // TODO: error state
                }
            },
            function (xhr, status) {
                console.log (xhr);
                console.log (status);
                // TODO: error state
            }
        );
    };

    var init = function () {
        // wire up the default save and cancel buttons
        $(root).dnnQuickSettings({
            moduleId: moduleId,
            onSave: saveSettings,
            onCancel: cancelSettings
        });
        loadSettings();
    }

    return {
        init: init
    }
};