using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net;

namespace BingWallpaper {
    public partial class Main : Form {

        private static string imagePath;
        private static bool __FREQ_UPDATE = false;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);


        private void CCBox_KeyUp(object sender, KeyEventArgs e) {
            string cc = CCBox.Text;
            int len = cc.Length;
            if (len < 2 || len > 2 || e.KeyCode == Keys.Back) return;
            LoadImage(cc);
            Settings.Set("cc", cc);
        }

        private void FreqText_TextChanged(object sender, EventArgs e) {
            if (__FREQ_UPDATE) return;
            int value;
            if (!int.TryParse(FreqText.Text, out value)) {
                return;
            }
            value %= 1440;
            if (value < 1) {
                value = 1;
                FreqText.Text = value.ToString();
            }
            FreqTrack.Maximum = 30;
            FreqTrack.TickFrequency = 1;
            if (value > 30) {
                FreqTrack.Maximum = value;
                FreqTrack.TickFrequency = int.Parse(Math.Round(value * 0.10).ToString());
            }
            FreqTrack.Value = value;
            Settings.Set("freq", value);
        }

        private void FreqTrack_Scroll(object sender, EventArgs e) {
            SwitchAndRun(ref __FREQ_UPDATE, () => {
                FreqText.Text = FreqTrack.Value.ToString();
                Settings.Set("freq", FreqText);
            });
        }

        private void SwitchAndRun(ref bool trigger, Action func) {
            trigger = !trigger;
            func();
            trigger = !trigger;
        }
        private void FitBox_TextChanged(object sender, EventArgs e) {
            if (!Global.WallpaperStyleList.Contains(FitBox.Text)) { return; }
            Settings.Set("fit", FitBox);
        }

        private void ApplyButton_Click(object sender, EventArgs e) {
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true)) {
                registryKey.SetValue(@"WallpaperStyle", Global.WallpaperStyle[Settings.Fetch("fit")]);
                registryKey.SetValue(@"TileWallpaper", (Settings.Fetch("fit") == "Tile" ? "1" : "0"));
            }
            SystemParametersInfo(20, 0, imagePath, 0x01 | 0x02);
            CreateTask(Settings.Fetch("freq"));
            Settings.Set("applied", "true");
            Settings.Save();
            ToggleApply();
            MessageBox.Show("Wallpaper applied and task successfully created!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToggleApply() {
            ApplyButton.Text = Settings.Fetch("applied") == "true" ? "Re-apply" : "Apply";
        }

        public static void CreateTask(string freq) {
            using (var proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo {
                    FileName = "schtasks.exe",
                    Arguments = $"/create /tn \"BingWallpaper\" /tr \"{Application.ExecutablePath}\" /sc MINUTE /mo {freq} /st 04:00",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                proc.Start();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e) {
            LoadImage(Settings.Fetch("cc"));
        }

        private void ResetTaskButton_Click(object sender, EventArgs e) {
            using (var proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo {
                    FileName = "schtasks.exe",
                    Arguments = "/delete /tn BingWallpaper /f",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                proc.Start();
            }
            Settings.Set("applied","false");
            Settings.Save();
            ToggleApply();
            MessageBox.Show("Task successfully removed.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void LoadImage(string cc) {
            BingHomepage homepage = new BingHomepage(cc);
            imagePath = Path.Combine(Global.Directory, $"image-{new Random().Next()}.bw");
            imagePreview.Image = homepage.GetImage(imagePath);
            InfoLabel.Text = homepage.GetCopyright;
        }

        public Main() {
            InitializeComponent();
            try {
                Settings.Create();
            } catch (Exception) {
                var result = MessageBox.Show("Some settings might be corrupt. Attempt fixing them?", "Bing Wallpaper", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes) {
                    Settings.Create(true);
                    return;
                } else {
                    throw new Exception("Unable to fix corrupt settings.");
                }
            }
        }

        private void Main_Load(object sender, EventArgs e) {
            int freq=int.Parse(Settings.Fetch("freq"))%1440;
            if (!Global.WallpaperStyle.Keys.ToArray().Contains(Settings.Fetch("fit"))) {
                Settings.Set("fit", "Stretch");
            }
            FitBox.SelectedItem = Settings.Fetch("fit");
            CCBox.Text = Settings.Fetch("cc");
            FreqText.Text = freq.ToString();
            FreqTrack.Value = freq;
            BatteryRunCheckBox.Checked = bool.Parse(Settings.Fetch("battery"));
            if (Settings.Fetch("applied") == "true") {
                ApplyButton.Text = "Re-apply";
            }
            LoadImage(Settings.Fetch("cc"));
        }
    }
}