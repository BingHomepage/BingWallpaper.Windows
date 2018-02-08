
# BingWallpaper [![Build status](https://ci.appveyor.com/api/projects/status/xjgr7vadeh42xc4l?svg=true)](https://ci.appveyor.com/project/muhammadmuzzammil1998/bingwallpaper)
Fetches and applies the image of the day from Bing as the wallpaper for Windows.

## Running
### Step 1: [Download the release binary from releases page](https://github.com/muhammadmuzzammil1998/BingWallpaper/releases).
### Step 2: Extract files to a safe place and do not delete the files.
### Troubleshooting
Execute this in either PowerShell or Command Prompt and run the app again.
```
schtasks.exe /delete /TN BingWallpaper
```
Still not fixed? [Contact me](https://github.com/muhammadmuzzammil1998/BingWallpaper/issues/new).

## Building
### Step 1: Clone the repository
```
git clone https://github.com/muhammadmuzzammil1998/BingWallpaper.git
cd BingWallpaper
```
### Step 2: Open BingWallpaper.sln file
### Step 3: Add references to (skip if no error message is shown):
- BingHomePageAPI
- System
- System.Linq
- System.IO
- System.Runtime.InteropServices
- Microsoft.Win32
- System.Windows.Forms
- System.Threading
- System.Diagnostics
- System.Drawing
### Step 4: Build!
Hit `F5` to start compiling and running the app.

## [For macOS and Ubuntu](https://github.com/nabeelomer/BingWallpapers)

## License
Licensed under MIT License by (c) Muhammad Muzzammil 2017 (http://muzzammil.xyz/)

Permission is hereby granted, free of charge, to any person obtaining a copy of this 
software and associated documentation files (the "Software"), to deal in the Software 
without restriction, including without limitation the rights to use, copy, modify, 
merge, publish, distribute, sublicense, and/or sell copies of the Software, and to 
permit persons to whom the Software is furnished to do so, subject to the following 
conditions:

The above copyright notice and this permission notice shall be included in all copies 
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR 
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.
