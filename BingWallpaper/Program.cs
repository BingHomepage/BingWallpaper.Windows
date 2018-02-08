/*
 * MIT License
 *
 * Copyright (c) 2017 Muhammad Muzzammil (http://muzzammil.xyz/)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using System;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using BingHomepageAPI;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace BingWallpaper {
    class Program {
        //Also requires BingHomepageAPI.dll (git.muzzammil.xyz/bing)
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        static void Main(string[] args) {
            string
                dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Bing Wallpapers/",
                path = dir + new Random().Next() + ".jpg";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            new BingHomepage().GetImage(path).Dispose();
            using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true)) {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            };
            SystemParametersInfo(20, 0, path, 0x01 | 0x02);
            Thread.Sleep(1000 * 10); //Wait for 10 seconds.
            foreach (var file in new DirectoryInfo(dir).GetFiles("*", SearchOption.TopDirectoryOnly).Where(fi => fi.Name != Path.GetFileName(path)))
                file.Delete();
            //Adding task to Task Scheduler to run the app for updating the wallpaper every 1 minute.
            using (Process proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo() {
                    FileName = "schtasks.exe",
                    Arguments = "/create /tn \"BingWallpaper\" /tr \"" + Application.ExecutablePath + "\" /sc MINUTE  /st 04:00",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                proc.Start();
            }
            //Killing all tasks.
            using (Process proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo() {
                    FileName = "taskkill",
                    Arguments = "/f /im schtasks.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                proc.Start();
            }
            Environment.Exit(0);
        }
    }
}
