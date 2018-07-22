# BingWallpaper [![Build status](https://ci.appveyor.com/api/projects/status/xjgr7vadeh42xc4l?retina=true)](https://ci.appveyor.com/project/muhammadmuzzammil1998/bingwallpaper)

Fetches and applies the image of the day from Bing as the wallpaper for Windows.

## Running

### Step 1: Download the zip file from [releases](https://github.com/BingHomepage/BingWallpaper.Windows/releases) page.

### Step 2: Extract files to a safe place and do not delete the files.

### Step 3: Run BingWallpaper.exe as admin and wait for 10 seconds. The wallpaper should get updated.

### Troubleshooting

Execute this in either PowerShell or Command Prompt and run the app again.

```batch
schtasks.exe /delete /TN BingWallpaper
```

Still not fixed? [Contact me](https://github.com/BingHomepage/BingWallpaper.Windows/issues/new).

## Building

BingWallpaper for windows uses [BingHomepage.CSharp](https://github.com/BingHomepage/BingHomepage.CSharp) library to fetch details.

### Step 1: Clone the repository

```batch
git clone https://github.com/BingHomepage/BingWallpaper.Windows.git
cd BingWallpaper.Windows
```

### Step 2: Open BingWallpaper.sln file

### Step 3: Add references to (skip if no error message is shown):

```csharp
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
```

### Step 4: Build!

Hit `F5` to start compiling and running the app.
