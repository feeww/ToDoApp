using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace To_Do_List.Services
{
    public class SettingsService : ISettingsService
    {
        private const string SETTINGS_FILE = "settings.json";
        private AppSettings _settings;

        private class AppSettings
        {
            public bool IsDarkTheme { get; set; } = false;
        }

        public SettingsService()
        {
            LoadSettings();
        }

        public bool GetThemeSetting()
        {
            return _settings.IsDarkTheme;
        }

        public void SaveThemeSetting(bool isDarkTheme)
        {
            _settings.IsDarkTheme = isDarkTheme;
            SaveSettings();
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(SETTINGS_FILE))
                {
                    string json = File.ReadAllText(SETTINGS_FILE);
                    _settings = JsonSerializer.Deserialize<AppSettings>(json);
                }
                else
                {
                    _settings = new AppSettings();
                }
            }
            catch (Exception)
            {
                _settings = new AppSettings();
            }
        }

        private void SaveSettings()
        {
            try
            {
                string json = JsonSerializer.Serialize(_settings);
                File.WriteAllText(SETTINGS_FILE, json);
            }
            catch (Exception)
            {
            }
        }
    }
}