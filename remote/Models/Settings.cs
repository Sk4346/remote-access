using System;

namespace remote
{
	public class Settings
	{
		public Settings ()
		{
		}


		public bool AlwaysUseDefaultResolution { get; set; }
		public bool AlwaysUseAero { get; set; }
		public bool AlwaysRedirectClipboard { get;set; }
		public int ResolutionHeight { get;set; }
		public int ResolutionWidth { get;set; }
	}
}

