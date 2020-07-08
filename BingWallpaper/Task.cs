using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace BingWallpaper {
    public static class Task {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private static void TaskSch(string arg, Action action) {
            using (var proc = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "schtasks.exe",
                    Arguments = arg,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            }) proc.Start();
            action();
        }

        public static void Create(string freq) =>
            TaskSch(
                $"/create /tn BingWallpaper /tr \"{Application.ExecutablePath} -cli\" /sc MINUTE /mo {freq} /st 04:00",
                () => {
                    Run();
                    Settings.Set("applied", true);
                    Settings.Save();
                });

        public static void Delete() => TaskSch("/delete /tn BingWallpaper /f", () => {
            Settings.Set("applied", false);
            Settings.Save();
        });

        public static void Run() {
            var homepage = new BingHomepage(Settings.Fetch("cc"));
            Global.Image = Path.Combine(Global.Directory, $"image-{new Random().Next()}.bw");
            homepage.GetImage(Global.Image);

            using (var registryKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true) ??
                                     throw new Exception("Unable to find registry key.")) {
                registryKey.SetValue(@"WallpaperStyle", Global.WallpaperStyle[Settings.Fetch("style")]);
                registryKey.SetValue(@"TileWallpaper", Settings.Fetch("style") == "Tile" ? "1" : "0");
            }

            SystemParametersInfo(20, 0, Global.Image, 0x01 | 0x02);
        }
    }
}