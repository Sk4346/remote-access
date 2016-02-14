using Gtk;
using Gdk;
using System;
using System.IO;
using GLib;
using System.Collections.Generic;
using System.Linq;

namespace remote
{
	class MainClass
	{

		// The tray Icon
		private static StatusIcon trayIcon;
		// MachineService
		private static MachineService machineService = new MachineService();

		public static void Main (string[] args)
		{
			
			// Initialize GTK#
			Application.Init ();

			// Creation of the Icon
			trayIcon = new StatusIcon(new Pixbuf ("Images/favicon.ico"));
			trayIcon.Visible = true;

			// Show/Hide the window (even from the Panel/Taskbar) when the TrayIcon has been clicked.
			//trayIcon.Activate += delegate { window.Visible = !window.Visible; };
			// Show a pop up menu when the icon has been right clicked.
			trayIcon.PopupMenu += OnTrayIconPopup;

			// A Tooltip for the Icon
			trayIcon.Tooltip = "Hello World Icon";

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
							System.Diagnostics.Process.Start("remote", "normal " + machine.FileName);
						} else if (machine.RemoteType == RemoteType.SSH) {
							System.Diagnostics.Process.Start("terminator", "-e 'remote normal " + machine.FileName + "'");
						} else if (machine.RemoteType == RemoteType.VNC) {
							System.Diagnostics.Process.Start("vncviewer", machine.FileName);
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
