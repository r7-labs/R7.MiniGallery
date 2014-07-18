using System;
					
using System.ComponentModel;
using System.Collections;
using DotNetNuke.Entities.Modules;

namespace R7.MiniGallery
{
	/// <summary>
	/// Provides strong typed access to settings used by module
	/// </summary>
	public partial class MiniGallerySettings
	{
		protected ModuleController ctrl;
		protected int tabModuleId;
		protected int moduleId;
    
		/// <summary>
		/// Initializes a new instance of the <see cref="MiniGallery.MiniGallerySettings"/> class.
		/// </summary>
		/// <param name='moduleId'>
		/// Module identifier.
		/// </param>
		/// <param name='tabModuleId'>
		/// TabModule identifier.
		/// </param>
		public MiniGallerySettings (int moduleId, int tabModuleId)
		{
			ctrl = new ModuleController (); 
			this.tabModuleId = tabModuleId;
			this.moduleId = moduleId;
		}

		/// <summary>
		/// Reads module setting.
		/// </summary>
		/// <returns>
		/// The setting value.
		/// </returns>
		/// <param name='settingName'>
		/// Setting name.
		/// </param>
		/// <param name='defaultValue'>
		/// Default value for setting.
		/// </param>
		/// <param name='tabSpecific'>
		/// If set to <c>true</c>, read tab-specific setting.
		/// </param>
		/// <typeparam name='T'>
		/// Type of the setting
		/// </typeparam>
		protected T ReadSetting<T> (string settingName, T defaultValue, bool tabSpecific)
		{
			var settings = (tabSpecific) ? 
            	ctrl.GetTabModuleSettings (this.tabModuleId) :
            	ctrl.GetModuleSettings (this.moduleId);
           
			T ret = default(T);

			if (settings.ContainsKey (settingName)) {
				var tc = TypeDescriptor.GetConverter (typeof(T));
				try {
					ret = (T)tc.ConvertFrom (settings [settingName]);
				} catch {
					ret = defaultValue;
				}
			} else
				ret = defaultValue;

			return ret;
		}
	
		/// <summary>
		/// Writes module setting.
		/// </summary>
		/// <param name='settingName'>
		/// Setting name.
		/// </param>
		/// <param name='value'>
		/// Setting value.
		/// </param>
		/// <param name='tabSpecific'>
		/// If set to <c>true</c>, setting is for this module on current tab.
		/// If set to <c>false</c>, setting is for this module on all tabs.
		/// </param>
		protected void WriteSetting<T> (string settingName, T value, bool tabSpecific)
		{
			if (tabSpecific)
				ctrl.UpdateTabModuleSetting (this.tabModuleId, settingName, value.ToString());
			else
				ctrl.UpdateModuleSetting (this.moduleId, settingName, value.ToString());
		}
	}
}

