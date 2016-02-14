using System;
using System.Configuration;

namespace remote
{
	public class SettingsService
	{
		public SettingsService ()
		{
		}


		public Settings GetSettings() {
			Settings settings = new Settings();

			bool UseDefaultResolution = false;
			bool.TryParse (ConfigurationManager.AppSettings ["AlwaysUseDefaultResolution"], out UseDefaultResolution);

			bool RedirectClipboard = false;
			bool.TryParse (ConfigurationManager.AppSettings ["AlwaysRedirectClipboard"], out RedirectClipboard);

			bool UseAero = false;
			bool.TryParse (ConfigurationManager.AppSettings ["AlwaysUseAero"], out UseAero);

			int ResolutionWidth = 0;
			int.TryParse(ConfigurationManager.AppSettings ["DefaultResolution"].Split('x')[0], out ResolutionWidth);

			int ResolutionHeight = 0;
			int.TryParse(ConfigurationManager.AppSettings ["DefaultResolution"].Split('x')[1], out ResolutionHeight);

			settings.AlwaysUseDefaultResolution = UseDefaultResolution;
			settings.AlwaysUseAero = UseAero;
			settings.ResolutionWidth = ResolutionWidth;
			settings.ResolutionHeight = ResolutionHeight;
			settings.AlwaysRedirectClipboard = RedirectClipboard;

			return settings;
		}

	}
}

