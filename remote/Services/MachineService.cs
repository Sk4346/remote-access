using System;
using System.IO;
using System.Collections.Generic;

namespace remote
{
	public class MachineService
	{
		public MachineService ()
		{
			
		}

		public List<Machine> GetAllMachines() {
			List<Machine> machines = new List<Machine> ();

			string[] machineFiles = Directory.GetFiles ("machines");
			foreach (string machineFile in machineFiles) {
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
						machine.FileName = machineFile.Replace("machines", "").Replace("/","").Replace("\\","");
					}
				}
				machines.Add (machine);
			}
			return machines;
		}

	}
}

