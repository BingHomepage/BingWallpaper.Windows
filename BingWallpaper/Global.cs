using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingWallpaper {
    public static class Global {
        public static string Directory =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Bing Wallpaper");
        public static string CountryCode => new RegionInfo(CultureInfo.CurrentCulture.LCID).Name;

        public static Dictionary<string, string> WallpaperStyle => new Dictionary<string, string>() {
            {"Fill", "10"}, {"Fit", "6"}, {"Stretch", "2"}, {"Tile", "0"}, {"Center", "0"}, {"Span", "22"}
        };

        public static string[] WallpaperStyleList => WallpaperStyle.Keys.ToArray();
    }
}
