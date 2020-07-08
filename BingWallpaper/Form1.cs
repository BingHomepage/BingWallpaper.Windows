using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;

namespace BingWallpaper {
    public partial class Main : Form {

        private static string _imagePath;
        private static bool _freqUpdate;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        private void ToggleApply() => ApplyButton.Text = Settings.Fetch("applied") == "true" ? "Re-apply" : "Apply";

        public Main() {
            InitializeComponent();
            try {
                Settings.Create();
            }
            catch (Exception) {
                var result = MessageBox.Show("Some settings might be corrupt. Attempt fixing them?", "Bing Wallpaper",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result != DialogResult.Yes) throw new Exception("Unable to fix corrupt settings.");
                Settings.Create(true);
            }
        }

        private void Main_Load(object sender, EventArgs e) {
            int freq = int.Parse(Settings.Fetch("freq")) % 1440;
            if (!Global.WallpaperStyleList.Contains(Settings.Fetch("style"))) {
                Settings.Set("style", "Stretch");
            }

            FitBox.SelectedItem = Settings.Fetch("style");
            CCBox.Text = Settings.Fetch("cc");
            FreqText.Text = freq.ToString();
            FreqTrack.Value = freq;
            BatteryRunCheckBox.Checked = bool.Parse(Settings.Fetch("battery"));
            ToggleApply();

            LoadImage(Settings.Fetch("cc"));
        }

        private void CCBox_KeyUp(object sender, KeyEventArgs e) {
            string cc = CCBox.Text;
            int len = cc.Length;
            if (len < 2 || len > 2 || e.KeyCode == Keys.Back) return;
            LoadImage(cc);
            Settings.Set("cc", cc);
        }

        private void FreqText_TextChanged(object sender, EventArgs e) {
            if (_freqUpdate) return;
            if (!int.TryParse(FreqText.Text, out int value)) return;
            if ((value %= 1440) < 1) {
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
            _freqUpdate = true;
            FreqText.Text = FreqTrack.Value.ToString();
            Settings.Set("freq", FreqText);
            _freqUpdate = false;
        }

        private void FitBox_TextChanged(object sender, EventArgs e) {
            if (!Global.WallpaperStyleList.Contains(FitBox.Text)) return;
            Settings.Set("style", FitBox);
        }

        private void ApplyButton_Click(object sender, EventArgs e) {
            using (var registryKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true) ??
                                     throw new Exception("Unable to find registry key.")) {
                registryKey.SetValue(@"WallpaperStyle", Global.WallpaperStyle[Settings.Fetch("style")]);
                registryKey.SetValue(@"TileWallpaper", (Settings.Fetch("style") == "Tile" ? "1" : "0"));
            }

            SystemParametersInfo(20, 0, _imagePath, 0x01 | 0x02);
            CreateTask(Settings.Fetch("freq"));
            Settings.Set("applied", "true");
            Settings.Save();
            ToggleApply();
            MessageBox.Show("Wallpaper applied and task successfully created!", "Success!", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static void CreateTask(string freq) {
            using (var proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo {
                    FileName = "schtasks.exe",
                    Arguments =
                        $"/create /tn \"BingWallpaper\" /tr \"{Application.ExecutablePath}\" /sc MINUTE /mo {freq} /st 04:00",
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

            Settings.Set("applied", "false");
            Settings.Save();
            ToggleApply();
            MessageBox.Show("Task successfully removed.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void LoadImage(string cc) {
            BingHomepage homepage = new BingHomepage(cc);
            _imagePath = Path.Combine(Global.Directory, $"image-{new Random().Next()}.bw");
            imagePreview.Image = homepage.GetImage(_imagePath);
            InfoLabel.Text = homepage.GetCopyright;
        }
    }
}