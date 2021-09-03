# Unity Package Extractor by Frost Sheridan

![screenshot](https://u.cubeupload.com/Frostion/Screenshot2021090301.png)

## What is this?

Unity Package Extractor is a simple program written in C# and .NET that can list and extract files from Unity packages.

As a frequent NeosVR user, I've always found it a bit annoying how most VR avatar artists put their work up for download as Unity packages only. Whenever I want to try out a new avatar or asset in Neos, I have to start a new Unity project and import the package *just* to grab the model and textures out of it. I eventually got so mildly annoyed by that inconvenience that I wrote this little utility to make the process significantly less painful.

## How to use it

To open a Unity package, you can click on the "Open Package" button to bring up a file dialog or simply drag and drop a .unitypackage file right into the window.

The program will display a list of all files and folders in the package. To extract these items, you can drag and drop them from the window into another folder/location, or click "Extract Selected"/"Extract All" to choose a location to copy the selected file/folder (or everything in the package) to.