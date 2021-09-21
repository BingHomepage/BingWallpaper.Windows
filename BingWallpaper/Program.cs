using System;
using System.Windows.Forms;

namespace BingWallpaper {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            if (args.Length > 0 && args[0] == "once") {
                Global.Log("Started headless instance");
                Task.Run(args.Length > 1 ? args[1] : Settings.Fetch("cc"));
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
