using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BingWallpaper {
    public static class Global {
        public static string Directory =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Bing Wallpaper");

        public static string LogFile => Path.Combine(Directory, "logs.bw");

        public static string Image { get; set; }

        public static Dictionary<string, string> WallpaperStyle => new Dictionary<string, string> {
            {"Fill", "10"}, {"Fit", "6"}, {"Stretch", "2"}, {"Tile", "0"}, {"Center", "0"}, {"Span", "22"}
        };

        public static string[] WallpaperStyleList => WallpaperStyle.Keys.ToArray();

        public static void Log(string evt) {
            File.AppendAllText(LogFile, $"{DateTime.Now:h:mm:ss tt} {evt}\n");
        }
    }
}