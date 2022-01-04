# MoveWindow

## Introduction
MoveWindow is a litte command program that move your window on specific coordinates.

Example of command:
`Movewindow.exe -w "My Window Title with space" -x 100 -y 42`
`MoveWindow.exe -c C:\Windows\SysWOW64\win32calc.exe -x 42 -y 42`


## Parameters
-w : Name of the main window
-x : X position of the high-left corner
-y : Y position of the high-left corner
-t : Time in seconds before moving the app (optionnal)
-c : Path to the program for execute it
-p : Program parameters. Use it with -c (optionnal)
-wd : Program Working directory (optionnal)
-d : Debug Mode (optionnal)
-h : Get help (optionnal)


## Requirements
- Net.Framework 4.6+


## Sources
Thanks to :
https://rlbisbe.net/2013/11/19/moving-windows-programatically-with-windows-api-the-path-to-winresize/

You can find references to the Win32 API commands directly in the code comments
