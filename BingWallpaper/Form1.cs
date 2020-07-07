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

namespace BingWallpaper {
    public partial class Main : Form {
        private static readonly string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Bing Wallpaper"),
            settingsFile=Path.Combine(directory, "settings.bw"),
            defaultCC = new RegionInfo(CultureInfo.CurrentCulture.LCID).Name;
        private static bool
            __FREQ_UPDATE = false;
        private static string[]
            __VALID_FITS= { "Fill","Fit","Stretch","Tile","Center","Span"};

        private static Dictionary<string, string> settings = new Dictionary<string, string>(),
            defaultSettings = new Dictionary<string, string>(){
                {"fit", "stretch" },
                {"fit", "Stretch" },
                {"cc", defaultCC },
                {"freq", "10" },
                {"applied","false" },
                {"battery", "true" },
            };

        private void CreateSettings(bool force = false) {
            if (force) {
                LoadSettings(force);
                File.Delete(settingsFile);
            }
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(settingsFile)) {
                defaultSettings.ToList().ForEach(x => {
                    if (settings.ContainsKey(x.Key)) {
                       File.AppendAllText(settingsFile, $"{x.Key}={settings[x.Key]??x.Value}\n");
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
            if (!__VALID_FITS.Contains(FitBox.Text)) { return; }
            settings["fit"] = FitBox.Text;
        }

        public void InitializeSettings() {
            int freq=int.Parse(settings["freq"]);
            if (!__VALID_FITS.Contains(settings["fit"])) {
                settings["fit"] = "Stretch";
            }
            FitBox.SelectedItem = settings["fit"];
            CCBox.Text = settings["cc"];
            FreqText.Text = freq.ToString();
            FreqTrack.Value = freq;
            BatteryRunCheckBox.Checked = bool.Parse(settings["battery"]);
            if (settings["applied"] == "true") {
                ApplyButton.Enabled = false;
                ApplyButton.Text = "Applied";
            }
        }

        public void LoadImage(string cc) {
            BingHomepage homepage = new BingHomepage(cc);
            imagePreview.Image = homepage.GetImage(Path.GetTempFileName());
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