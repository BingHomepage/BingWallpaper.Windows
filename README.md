# ![Banner](./.github/graphics/banner.png)

<center style="font-family: 'Times New Roman';">
  <i>
    Fetches and applies the image of the day from Bing as the wallpaper for Windows.
  </i>
  <br>
  <img src="https://ci.appveyor.com/api/projects/status/huk5c0y25r4mrq10/branch/master?retina=true" alt="Build status"></img>
</center>

![Bing Wallpaper](./.github/images/BW.png)

## Installation

### Detailed steps

Step 1: Download the latest build from [releases](https://github.com/BingHomepage/BingWallpaper.Windows/releases) page and extract the `.zip` to a safe location where these files will not be deleted accidentally. You can extract it to `%appdata%/../Local/Bing Wallpaper`.

Step 2: Open the folder where you extracted the files. Right click `Bing Wallpaper.exe` to pin it to start and create a shortcut to desktop.

Step 3: Open `Bing Wallpaper.exe`. Ignore warnings and start the application.

Step 4: Click on Apply and congratulations! Your wallpaper will now be updated to the image shown. Your country code will be inferred from your locale but you can enter a different one. Tweak around the settings to find your best fit.

### TL; DR

Download from [releases](https://github.com/BingHomepage/BingWallpaper.Windows/releases) page. Extract to a safe location. Run `Bing Wallpaper.exe`. Apply.

## Settings

### Style

| Settings     | Description                                                                                                    |
| :----------- | :------------------------------------------------------------------------------------------------------------- |
| Choose a fit | Select the way wallpaper should fill your desktop. Default: Stretch.                                           |
| Country code | Enter an ISO alpha-2 country code which it should fetch the wallpaper for. Default: based on Windows Settings. |

### Task settings

| Settings  | Description                                                                                                                                                                                                                           |
| :-------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Frequency | Select the interval in minutes after which the application should update the wallpaper. This ranges from 1 minute to 1439 minutes. Please note too frequent might drain battery, ideal interval is an hour (60 minutes). Default: 30. |

### Options

| Settings   | Description                                                                   |
| :--------- | :---------------------------------------------------------------------------- |
| Apply      | Creates a task to update wallpaper with given settings and applies wallpaper. |
| Apply once | Updates wallpaper once without creating a recurring task.                     |
| Reset task | Deletes the task created by "Apply".                                          |
| Refresh    | Refreshes the current image.                                                  |

## Contributions

Contributions are welcome but kindly follow the Code of Conduct and guidelines. Please don't make Pull Requests for typographical errors, grammatical mistakes, "sane way" of doing it, etc. Open an issue for it. Thanks!
