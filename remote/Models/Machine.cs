using System;

namespace remote
{
	public class Machine
	{
		public Machine ()
		{
		}

		public string GroupName { get; set; }
		public string MachineName { get; set; }
		public RemoteType RemoteType { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string FileName { get; set; }
	}

	public enum RemoteType {
		RDP,
		SSH,
		VNC
	}
}

