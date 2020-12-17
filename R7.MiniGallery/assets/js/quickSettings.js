// TODO: Convert to React
var minigallery = minigallery || {};

minigallery.service = function ($, moduleId) {
    var baseServicePath = $.dnnSF(moduleId).getServiceRoot("R7.MiniGallery");
    this.ajaxCall = function (type, controller, action, id, data, success, fail) {
        $.ajax({
            type: type,
            url: baseServicePath + controller + "/" + action + (id != null ? "/" + id : ""),
            beforeSend: $.dnnSF(moduleId).setModuleHeaders,
            data: data
        }).done(function (retData) {
            if (success != undefined) {
                success(retData);
            }
        }).fail(function (xhr, status) {
            if (fail != undefined) {
                fail(xhr, status);
            }
        });
    };

    this.getSettings = function (success, fail) {
        this.ajaxCall("GET", "QuickSettings", "Get", null, null, success, fail);
    };

    this.updateSettings = function (success, fail, data) {
        this.ajaxCall("POST", "QuickSettings", "Update", null, data, success, fail);
    };
};

minigallery.quickSettings = function (root, moduleId) {

	var setFormData = function (data) {
		$("#r7_mg_qsettings_imageCssClass_" + moduleId).val (data.imageCssClass);
        $("#r7_mg_qsettings_numberOfRecords_" + moduleId).val (data.numberOfRecords);
        $("#r7_mg_qsettings_showTitles_" + moduleId).each (function () { this.checked = data.showTitles; });
	};

	var getFormData = function () {
		return {
			imageCssClass: $("#r7_mg_qsettings_imageCssClass_" + moduleId).val (),
            numberOfRecords: $("#r7_mg_qsettings_numberOfRecords_" + moduleId).val (),
            showTitles: $("#r7_mg_qsettings_showTitles_" + moduleId).is (":checked")
		}
	};

	var setError = function () {
		$("r7_mg_qsettings_" + moduleId).addClass ("error");
	};

	var isError = function () {
		$("r7_mg_qsettings_" + moduleId).hasClass ("error");
	}

	var updateSettings = function () {
		var deferred = $.Deferred();
		if (isError ()) {
			// disable further updates
			deferred.reject ();
			return deferred.promise ();
		}
		var service = new minigallery.service ($, moduleId);
        service.updateSettings (
            function (data) {
                // TODO: Update main view w/o page reload
                deferred.resolve ();
                document.location.reload (true);
            },
            function (xhr, status) {
                deferred.reject ();
                console.log (xhr);
                console.log (status);
                setError ();
            },
            getFormData ()
        );

        return deferred.promise ();
    };

    var loadSettings = function () {
        var service = new minigallery.service ($, moduleId);
        var settings = service.getSettings (
            function (data) {
                setFormData (data);
            },
            function (xhr, status) {
                console.log (xhr);
                console.log (status);
                setError ();
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

// basic export
window.minigallery = minigallery;
