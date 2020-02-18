using Newtonsoft.Json;

namespace R7.MiniGallery.ViewModels
{
    [JsonObject (MemberSerialization.OptIn)]
    public class QuickSettingsViewModel
    {
        [JsonProperty ("imageCssClass")]
        public string ImageCssClass { get; set; }

        [JsonProperty ("numberOfRecords")]
        public int NumberOfRecords { get; set; }

        [JsonProperty ("showTitles")]
        public bool ShowTitles { get; set; }
    }
}
