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

		// The tray Icon
		private static StatusIcon trayIcon;
		private static Settings settings;
		// MachineService
		private static MachineService machineService = new MachineService();
		private static SettingsService settingsService = new SettingsService();

		public static void Main (string[] args)
		{
			settings = settingsService.GetSettings ();

			// Initialize GTK#
			Application.Init ();

			// Creation of the Icon
			trayIcon = new StatusIcon(new Pixbuf ("favicon.ico"));
			trayIcon.Visible = true;

			// Show/Hide the window (even from the Panel/Taskbar) when the TrayIcon has been clicked.
			//trayIcon.Activate += delegate { window.Visible = !window.Visible; };
			// Show a pop up menu when the icon has been right clicked.
			trayIcon.PopupMenu += OnTrayIconPopup;

			// A Tooltip for the Icon
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
					MenuItem menuItem = new MenuItem (machine.FileName);
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
			// Quit the application when quit has been clicked.
			menuItemQuit.Activated += delegate { Application.Quit(); };
			popupMenu.ShowAll();
			popupMenu.Popup();
		}


	}
}
