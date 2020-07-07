using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingWallpaper {
    public static class Settings {
        private static readonly string SettingsFile = Path.Combine(Global.Directory, "settings.bw");
        private static Dictionary<string, string> _settings = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> DefaultSettings = new Dictionary<string, string>() {
                {"fit", "Stretch"},
                {"cc", Global.CountryCode},
                {"freq", "10"},
                {"applied", "false"},
                {"battery", "true"},
            };

        public static string Fetch(string key) => _settings[key];
        public static void Set(string key, string value) => _settings[key] = value;
        public static void Set(string key, int value) => Set(key, value.ToString());
        public static void Set(string key, Control c) => Set(key, c.Text);
        public static void Create(bool force = false, Dictionary<string, string> dSettings = null) {
            if (force ^ dSettings != null) {
                Load(force);
                File.Delete(SettingsFile);
            }
            if (!Directory.Exists(Global.Directory)) {
                Directory.CreateDirectory(Global.Directory);
            }
            if (!File.Exists(SettingsFile)) {
                (dSettings ?? DefaultSettings).ToList().ForEach(x => {
                    if (_settings.ContainsKey(x.Key)) {
                        File.AppendAllText(SettingsFile, $"{x.Key}={_settings[x.Key] ?? x.Value}\n");
                        return;
                    }
                    File.AppendAllText(SettingsFile, $"{x.Key}={x.Value}\n");
                });
            }
            Load();
        } 

        public static void Load(bool forceRead = false) {
            File.ReadAllLines(SettingsFile).ToList().ForEach(x => {
                if (x.Length == 0) return;
                var data = x.Split('=').Where(y=>y.Trim()!="").ToArray();
                if (data.Length < 2) {
                    if (!forceRead) {
                        throw new Exception("Invalid settings");
                    }
                    data = new string[] { data[0], null };
                }
                _settings[data[0]] = data[1];
            });
            if (_settings.Count < DefaultSettings.Count && !forceRead) {
                throw new Exception("Insufficient settings");
            }
        }

        public static void Save() {
            File.Delete(SettingsFile);
            Create(true, _settings);
            Load();
        }
    }
}
