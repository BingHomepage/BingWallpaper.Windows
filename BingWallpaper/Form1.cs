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
        private Dictionary<string, string> settings = new Dictionary<string, string>();
        private static readonly string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Bing Wallpaper"),
            settingsFile=Path.Combine(directory, "settings.bw"),
            defaultCC = new RegionInfo(CultureInfo.CurrentCulture.LCID).Name;

        private void CreateSettings(bool force = false) {
            if (force) {
                File.Delete(settingsFile);
            }
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(settingsFile)) {
                File.WriteAllText(settingsFile, $"cc={defaultCC}");
            }
        }
        private void LoadSettings() {
            CreateSettings();
            File.ReadAllLines(settingsFile).ToList().ForEach(x => {
                var data = x.Split('=').Where(y=>y.Trim()!="").ToArray();
                if (data.Length < 2) {
                    var result = MessageBox.Show("Settings might be corrupt. Reset them?", "Bing Wallpaper", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (result == DialogResult.Yes) {
                        CreateSettings(true);
                    } else {
                        throw new Exception("Corrupt settings. Exiting...");
                    }
                    return;
                }
                settings[data[0]] = data[1];
            });
        }
        public Main() {
            InitializeComponent();
            try {
                LoadSettings();
            } catch (Exception exp) {
                MessageBox.Show(exp.Message, "Bing Wallpaper", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Main_Load(object sender, EventArgs e) {
            BingHomepage homepage = new BingHomepage();
            imagePreview.Image = homepage.GetImage(Path.GetTempFileName());
        }
    }
}