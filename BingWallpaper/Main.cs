using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BingWallpaper {
    public partial class Main : Form {
        private static bool _freqUpdate;
        private void ToggleApply() => ApplyButton.Text = Settings.Fetch("applied") == "true" ? "Re-apply" : "Apply";

        private void LoadImage(string cc) {
            Loading.Visible = true;
            BingHomepage homepage = new BingHomepage(cc);
            Global.Image = Path.Combine(Global.Directory, $"image-{new Random().Next()}.bw");
            imagePreview.Image = homepage.GetImage(Global.Image);
            InfoLabel.Text = homepage.GetCopyright;
            Loading.Visible = false;
            Global.Log($"Load image for {cc}");
        }

        public Main() {
            InitializeComponent();
            try {
                Settings.Create();
            }
            catch (Exception exp) {
                var result = MessageBox.Show("Some settings might be corrupt. Attempt fixing them?", "Bing Wallpaper",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result != DialogResult.Yes) throw new Exception($"Unable to fix corrupt settings.\n{exp.Message}");
                Settings.Create(true);
            }
        }

        private void Main_Load(object sender, EventArgs e) {
            Global.Log("Started GUI instance");
            int freq = int.Parse(Settings.Fetch("freq")) % 1440;
            if (!Global.WallpaperStyleList.Contains(Settings.Fetch("style"))) {
                Settings.Set("style", "Stretch");
            }

            FitBox.SelectedItem = Settings.Fetch("style");
            CCBox.Text = Settings.Fetch("cc");
            FreqText.Text = freq.ToString();
            FreqTrack.Value = freq;
            ToggleApply();

            LoadImage(Settings.Fetch("cc"));
        }

        private void CCBox_KeyUp(object sender, KeyEventArgs e) {
            string cc = CCBox.Text;
            int len = cc.Length;
            if (len < 2 || len > 2 || e.KeyCode == Keys.Back) return;
            LoadImage(cc);
            Settings.Set("cc", cc.ToUpper());
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
            Task.Create(Settings.Fetch("freq"));
            Task.Run();
            Settings.Set("applied", true);
            Settings.Save();
            ToggleApply();
            MessageBox.Show("Wallpaper applied and task successfully created!", "Success!", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void RefreshButton_Click(object sender, EventArgs e) {
            LoadImage(Settings.Fetch("cc"));
        }

        private void ResetTaskButton_Click(object sender, EventArgs e) {
            Task.Delete();
            Settings.Set("applied", false);
            Settings.Save();
            ToggleApply();
            MessageBox.Show("Task successfully removed.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ApplyOnce_Click(object sender, EventArgs e) {
            Task.Run();
        }

        private void AboutButton_Click(object sender, EventArgs e) {
            new About().Show();
        }

        private void MMK_Click(object sender, EventArgs e) {
            Process.Start("https://muzzammil.xyz");
        }
    }
}