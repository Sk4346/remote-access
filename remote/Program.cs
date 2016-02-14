using Gtk;
using Gdk;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace remote
{
	class MainClass
	{

		private static StatusIcon trayIcon;
		private static Settings settings;
		private static MachineService machineService = new MachineService();
		private static SettingsService settingsService = new SettingsService();

		public static void Main (string[] args)
		{
			settings = settingsService.GetSettings ();
			Application.Init ();

			string iconPath = settings.ExecutingPath + "favicon.ico";

			trayIcon = new StatusIcon (new Pixbuf (iconPath));
			trayIcon.PopupMenu += OnTrayIconPopup;
			trayIcon.Visible = true;
			trayIcon.Tooltip = "Remote Access";


			if (Directory.Exists ("machines") == false) {
				Directory.CreateDirectory ("machines");
			}


			Application.Run ();
		}


		// Create the popup menu, on right click.
		static void OnTrayIconPopup (object o, EventArgs args) {
			Menu popupMenu = new Menu();
			ImageMenuItem menuItemQuit = new ImageMenuItem ("Quit");
			Gtk.Image appimg = new Gtk.Image(Stock.Quit, IconSize.Menu);
			menuItemQuit.Image = appimg;

			var machines = machineService.GetAllMachines ()
				.GroupBy (x => x.GroupName);
			foreach (var item in machines) {
				if (item.Key == null)
					continue;

				MenuItem parentItem = new MenuItem (item.Key);
				Menu subMenu = new Menu ();
				foreach (Machine machine in item) {
//					ImageMenuItem menuItem = new ImageMenuItem (machine.FileName);
//					menuItem.Image = new Gtk.Image (Stock.Connect, IconSize.Menu);
					MenuItem menuItem = new MenuItem (machine.RemoteType + " - " + machine.FileName);
					menuItem.Activated += (object sender, EventArgs e) => {
						if (machine.RemoteType == RemoteType.RDP) {
							machineService.LaunchRDP(machine, settings);
						} else if (machine.RemoteType == RemoteType.SSH) {
							machineService.LaunchSSH(machine, settings);
						} else if (machine.RemoteType == RemoteType.VNC) {
							machineService.LaunchVNC(machine, settings);
						}
					};
					subMenu.Add (menuItem);
				}
				parentItem.Submenu = subMenu;
				popupMenu.Add (parentItem);
			}


			popupMenu.Add(menuItemQuit);

			menuItemQuit.Activated += delegate { Application.Quit(); };
			popupMenu.ShowAll();
			popupMenu.Popup();
		}


	}
}