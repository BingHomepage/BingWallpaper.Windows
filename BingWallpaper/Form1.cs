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
        private static readonly string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Bing Wallpaper"),
            settingsFile=Path.Combine(directory, "settings.bw"),
            defaultCC = new RegionInfo(CultureInfo.CurrentCulture.LCID).Name;
        private static string imagePath;
        private static bool
            __FREQ_UPDATE = false;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private static Dictionary<string, string> settings = new Dictionary<string, string>(),
            defaultSettings = new Dictionary<string, string>(){
                {"fit", "Stretch" },
                {"cc", defaultCC },
                {"freq", "10" },
                {"applied","false" },
                {"battery", "true" },
            },
            wallpaperStyle= new Dictionary<string, string>(){
                {"Fill", "10" },{"Fit", "6"}, {"Stretch", "2"}, { "Tile","0"},{"Center","0" },{ "Span","22"}
            };

        private void CreateSettings(bool force = false, Dictionary<string, string> dSettings = null) {
            if (force^dSettings!=null) {
                LoadSettings(force);
                File.Delete(settingsFile);
            }
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(settingsFile)) {
                (dSettings ?? defaultSettings).ToList().ForEach(x => {
                    if (settings.ContainsKey(x.Key)) {
                        File.AppendAllText(settingsFile, $"{x.Key}={settings[x.Key] ?? x.Value}\n");
                        return;
                    }
                    File.AppendAllText(settingsFile, $"{x.Key}={x.Value}\n");
                });
            }
            LoadSettings();
        }

        private void LoadSettings(bool forceRead = false) {
            File.ReadAllLines(settingsFile).ToList().ForEach(x => {
                if (x.Length == 0) return;
                var data = x.Split('=').Where(y=>y.Trim()!="").ToArray();
                if (data.Length < 2) {
                    if (!forceRead) {
                        throw new Exception("Invalid settings");
                    }
                    data = new string[] { data[0], null };
                }
                settings[data[0]] = data[1];
            });
            if (settings.Count < defaultSettings.Count && !forceRead) {
                throw new Exception("Insufficient settings");
            }
        }

        private void CCBox_KeyUp(object sender, KeyEventArgs e) {
            string cc = CCBox.Text;
            int len = cc.Length;
            if (len < 2 || len > 2 || e.KeyCode == Keys.Back) return;
            LoadImage(cc);
            settings["cc"] = cc;
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
            settings["freq"] = value.ToString();
        }

        private void FreqTrack_Scroll(object sender, EventArgs e) {
            SwitchAndRun(ref __FREQ_UPDATE, () => {
                FreqText.Text = FreqTrack.Value.ToString();
                settings["freq"] = FreqText.Text;
            });
        }

        private void SwitchAndRun(ref bool trigger, Action func) {
            trigger = !trigger;
            func();
            trigger = !trigger;
        }
        private void FitBox_TextChanged(object sender, EventArgs e) {
            if (!wallpaperStyle.Keys.ToArray().Contains(FitBox.Text)) { return; }
            settings["fit"] = FitBox.Text;
        }

        private void ApplyButton_Click(object sender, EventArgs e) {
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true)) {
                registryKey.SetValue(@"WallpaperStyle", wallpaperStyle[settings["fit"]]);
                registryKey.SetValue(@"TileWallpaper", (settings["fit"] == "Tile" ? "1" : "0"));
            }
            SystemParametersInfo(20, 0, imagePath, 0x01 | 0x02);
            CreateTask(settings["freq"]);
            settings["applied"] = "true";
            SaveSettings();
        }
        public static void CreateTask(string freq) {
            using (var proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo {
                    FileName = "schtasks.exe",
                    Arguments = $"/create /tn \"BingWallpaper\" /tr \"{Application.ExecutablePath}\" /sc MINUTE /mo ${freq} /st 04:00",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                proc.Start();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e) {
            LoadImage(settings["cc"]);
        }

        public void InitializeSettings() {
            int freq=int.Parse(settings["freq"])%1440;
            if (!wallpaperStyle.Keys.ToArray().Contains(settings["fit"])) {
                settings["fit"] = "Stretch";
            }
            FitBox.SelectedItem = settings["fit"];
            CCBox.Text = settings["cc"];
            FreqText.Text = freq.ToString();
            FreqTrack.Value = freq;
            BatteryRunCheckBox.Checked = bool.Parse(settings["battery"]);
            if (settings["applied"] == "true") {
                ApplyButton.Text = "Re-apply";
            }
        }

        private void ResetTaskButton_Click(object sender, EventArgs e) {
            using (var proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo {
                    FileName = "schtasks.exe",
                    Arguments = "/delete /tn \"BingWallpaper\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                proc.Start();
            }
            settings["applied"] = "false";
            SaveSettings();
        }

        public void SaveSettings() {
            File.Delete(settingsFile);
            CreateSettings(true, settings);
            LoadSettings();
            ApplyButton.Text = settings["applied"]=="true"?"Re-apply":"Apply";
        }

        public void LoadImage(string cc) {
            BingHomepage homepage = new BingHomepage(cc);
            imagePath = Path.Combine(directory, $"image-{new Random().Next()}.bw");
            imagePreview.Image = homepage.GetImage(imagePath);
            InfoLabel.Text = homepage.GetCopyright;
        }

        public Main() {
            InitializeComponent();
            try {
                CreateSettings();
            } catch (Exception) {
                var result = MessageBox.Show("Some settings might be corrupt. Attempt fixing them?", "Bing Wallpaper", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes) {
                    CreateSettings(true);
                    return;
                } else {
                    throw new Exception("Unable to fix corrupt settings.");
                }
            }
        }

        private void Main_Load(object sender, EventArgs e) {
            InitializeSettings();
            LoadImage(settings["cc"]);
        }
    }
}