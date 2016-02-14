This little applet is designed to be extremely simple and easy to use tray applet for freerdp/ssh/vnc and is cross-platform for Windows/Linux/OSX


Machine folder

The machines folder needs to exist in the executing assembly's directory. This folder will contain each of your machine files that you remote into. 

File format for freerdp
```
group=<mymachinesgroup>
machine=<ip/hostname>
username=<username>@<domain/ifnecessary>
password=<password>
type=rdp
```

File format for ssh
```
group=<mymachinesgroup>
machine=<ip/hostname>
username=<username>
password=<password>
type=ssh
```

File format for vnc
```
group=<mymachinesgroup>
machine<ip/hostname>
type=vnc
```

Starting the application automatically

For Linux : 
Add this line to your .xinitrc
`nohup mono remote.exe &`

For Windows : 
Move the `remote.exe` program to your Startup folder under your user profile

`C:\Users\<username>\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup`

For OSX :
More to come...
