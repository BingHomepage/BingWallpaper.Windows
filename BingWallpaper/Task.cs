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

        private static int Process(string cmd, string args) {
            int taskId;
            using (var process = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = cmd,
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            }) {
                process.Start();
                taskId = process.Id;
            }
            Global.Log($"Started process (PID: {taskId}) {cmd} {args}");
            return taskId;
        }

        private static void TaskSch(string args, Action action) {
            Process("taskkill", $"/f /pid {Process("schtasks", args)}");
            action();
        }

        public static void Create(string freq) =>
            TaskSch(
                $"/create /tn BingWallpaper /tr \"{Application.ExecutablePath} once\" /sc MINUTE /mo {freq} /st 04:00",
                () => {
                    Run();
                    Settings.Set("applied", true);
                    Settings.Save();
                });

        public static void Delete() => TaskSch("/delete /tn BingWallpaper /f", () => {
            Settings.Set("applied", false);
            Settings.Save();
        });

        public static void Run(string cc = null) {
            if (Global.Image == null) {
                var homepage = new BingHomepage(cc ?? Settings.Fetch("cc"));
                Global.Image = Path.Combine(Global.Directory, $"image-{new Random().Next()}.bw");
                homepage.GetImage(Global.Image);
            }

            using (var registryKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true) ??
                                     throw new Exception("Unable to find registry key.")) {
                registryKey.SetValue(@"WallpaperStyle", Global.WallpaperStyle[Settings.Fetch("style")]);
                registryKey.SetValue(@"TileWallpaper", Settings.Fetch("style") == "Tile" ? "1" : "0");
            }

            SystemParametersInfo(20, 0, Global.Image, 0x01 | 0x02);

            new DirectoryInfo(Global.Directory)
                .GetFiles("image-*.bw", SearchOption.TopDirectoryOnly)
                .ToList()
                .ForEach(file => {
                    try {
                        File.Delete(file.FullName);
                        Global.Log($"Deleted {file.Name}");
                    }
                    catch {
                        // ignored
                        Global.Log($"Unable to delete {file.Name}");
                    }
                });
        }
    }
}