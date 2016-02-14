using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace remote
{
	public class MachineService
	{
		
		public MachineService ()
		{
		}

		public void LaunchRDP(Machine machine, Settings settings) {
			Process freerdp = new Process();
			freerdp.StartInfo.FileName = "xfreerdp";
			freerdp.StartInfo.Arguments = BuildArguments(machine, settings);
			freerdp.Start();
		}

		public void LaunchSSH(Machine machine, Settings settings) {
			Process.Start("terminator", "-e 'ssh " + machine.Username + "@" + machine.MachineName + "'");
		}

		public void LaunchVNC(Machine machine, Settings settings) {
			Process.Start("vncviewer", machine.MachineName);
		}

		public string BuildArguments (Machine machine, Settings settings) {
			string arguments = string.Empty;
			if (settings.AlwaysUseDefaultResolution) {
				arguments += "/w:" + settings.ResolutionWidth + " ";
				arguments += "/h:" + settings.ResolutionHeight + " ";
			}

			if (settings.AlwaysRedirectClipboard) {
				arguments += "+clipboard ";
			} else {
				arguments += "-clipboard ";
			}

			if (settings.AlwaysUseAero) {
				arguments += "+aero ";
			} else {
				arguments += "-aero ";
			}

			arguments += "/u:" + machine.Username + " ";
			arguments += "/p:" + machine.Password + " ";
			arguments += "/v:" + machine.MachineName + " ";
			return arguments;
		}

		public List<Machine> GetAllMachines() {
			List<Machine> machines = new List<Machine> ();
			Settings settings = new SettingsService ().GetSettings ();

			string[] machineFiles = Directory.GetFiles (settings.ExecutingPath + "machines");
			foreach (string machineFile in machineFiles) {
				string machineFilePath = System.IO.Path.GetFileName (machineFile);
				Machine machine = new Machine ();
				string[] lines = File.ReadAllLines (machineFile);
				foreach (string line in lines) {
					string[] split = line.Split ('=');
					if (split.Length == 2) {
						string param = split [0];
						string value = split [1];

						if (param == "type") {
							RemoteType rType;
							Enum.TryParse (value.ToUpper (), out rType);
							machine.RemoteType = rType;
						}
						if (param == "username")
							machine.Username = value;
						if (param == "password")
							machine.Password = value;
						if (param == "machine")
							machine.MachineName = value;
						if (param == "group")
							machine.GroupName = value;
						machine.FileName = machineFilePath;
					}
				}
				machines.Add (machine);
			}
			return machines;
		}

	}
}

