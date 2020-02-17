using System;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.Api;
using R7.MiniGallery.Models;
using R7.MiniGallery.ViewModels;

namespace R7.MiniGallery.Services
{
    /// <summary>
    /// ContentTypeController provides the Web Services to manage Data Types
    /// </summary>
    [SupportedModules ("R7_MiniGallery")]
    [DnnModuleAuthorize (AccessLevel = SecurityAccessLevel.Edit)]
    public class QuickSettingsController: DnnApiController
    {
        protected readonly MiniGallerySettingsRepository SettingsRepository = new MiniGallerySettingsRepository ();

        [HttpGet]
        public HttpResponseMessage Get ()
        {
            try {
                var settings = SettingsRepository.GetSettings (ActiveModule);

                var quickSettings = new QuickSettingsViewModel {
                    ImageCssClass = settings.ImageCssClass,
                    NumberOfRecords = settings.NumberOfRecords
                };

                return Request.CreateResponse (quickSettings);
            }
            catch (Exception ex) {
                Exceptions.LogException (ex);
                return Request.CreateResponse (new { success = false });
            }
        }

        [HttpPost]
        public HttpResponseMessage Save (QuickSettingsViewModel quickSettings)
        {
            try {
                var settings = SettingsRepository.GetSettings (ActiveModule);

                settings.ImageCssClass = quickSettings.ImageCssClass;
                settings.NumberOfRecords = quickSettings.NumberOfRecords;

                SettingsRepository.SaveSettings (ActiveModule, settings);
                return Request.CreateResponse (new { success = true });
            }
            catch (Exception ex) {
                Exceptions.LogException (ex);
                return Request.CreateResponse (new { success = false });
            }
        }
    }
}