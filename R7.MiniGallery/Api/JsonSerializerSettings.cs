using Newtonsoft.Json;

namespace R7.MiniGallery
{
	class JsonSerializerSettings : Newtonsoft.Json.JsonSerializerSettings
	{
		public object ContractResolver { get; set; }
	}
}