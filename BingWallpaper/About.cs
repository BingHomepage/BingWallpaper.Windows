using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingWallpaper {
    public partial class About : Form {
        private static void Open(string url) => Process.Start(url);

        public About() {
            InitializeComponent();
            if (!File.Exists(Global.LogFile)) ClearLogs.Enabled = false;
        }

        private void Webpage_Click(object sender, EventArgs e) {
            Open("https://binghomepage.github.io/BingWallpaper.Windows/");
        }

        private void SourceCode_Click(object sender, EventArgs e) {
            Open("https://github.com/BingHomepage/BingWallpaper.Windows/");
        }

        private void ReportIssue_Click(object sender, EventArgs e) {
            Open("https://github.com/BingHomepage/BingWallpaper.Windows/issues");
        }

        private void OpenLogs_Click(object sender, EventArgs e) {
            Open(Global.LogFile);
        }

        private void Website_Click(object sender, EventArgs e) {
            Open("https://muzzammil.xyz");
        }

        private void ClearLogs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (!File.Exists(Global.LogFile)) return;
            File.Delete(Global.LogFile);
            MessageBox.Show("Logs cleared", "Bing Wallpaper", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearLogs.Enabled = false;
        }
    }
}