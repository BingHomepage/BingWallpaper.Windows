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
        private static Dictionary<string, string> settings = new Dictionary<string, string>(),
            defaultSettings = new Dictionary<string, string>(){
                {"fit", "stretch" },
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
        }

        public void InitializeSettings() {
            string[] validFits = { "Fill","Fit","Stretch","Tile","Center","Span"};
            int freq=int.Parse(settings["freq"]) % 60;
            if (!validFits.Contains(settings["fit"])) {
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