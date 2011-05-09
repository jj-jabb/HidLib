HidLib
=============

This is a refactor of https://github.com/mikeobrien/HidLibrary. It is a
significant deviation from the original project and uses a separate thread
for reading from the device.

There is a write thread that will allow writes asynchronously using a
delegate queue, but it hasn't been fully tested.

Requirements
------------
.NET 2.0 SP1 framework
Visual Studio C# 2008 to open project files