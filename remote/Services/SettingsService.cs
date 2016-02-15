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

			string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly ().Location);
			PlatformID CurrentPlatform = Environment.OSVersion.Platform;
			if (CurrentPlatform == PlatformID.MacOSX || CurrentPlatform == PlatformID.Unix) {
				path = path + "/";
			} else {
				path = path + "\\";
			}

			bool AlwaysUseFonts = false;
			bool.TryParse (ConfigurationManager.AppSettings ["AlwaysUseFonts"], out AlwaysUseFonts);

			bool AlwaysUseWindowDrag = false;
			bool.TryParse (ConfigurationManager.AppSettings ["AlwaysUseWindowDrag"], out AlwaysUseWindowDrag);

			bool AlwaysUseMenuAnims = false;
			bool.TryParse (ConfigurationManager.AppSettings ["AlwaysUseMenuAnims"], out AlwaysUseMenuAnims);

			bool AlwaysUseRFX = false;
			bool.TryParse (ConfigurationManager.AppSettings ["AlwaysUseRFX"], out AlwaysUseRFX);

			settings.AlwaysUseRFX = AlwaysUseRFX;
			settings.AlwaysUseWindowDrag = AlwaysUseWindowDrag;
			settings.AlwaysUseMenuAnims = AlwaysUseMenuAnims;
			settings.AlwaysUseFonts = AlwaysUseFonts;
			settings.CurrentPlatform = CurrentPlatform;
			settings.ExecutingPath = path;
			settings.AlwaysUseDefaultResolution = UseDefaultResolution;
			settings.AlwaysUseAero = UseAero;
			settings.ResolutionWidth = ResolutionWidth;
			settings.ResolutionHeight = ResolutionHeight;
			settings.AlwaysRedirectClipboard = RedirectClipboard;

			return settings;
		}

	}
}

