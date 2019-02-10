using CRUD_proj.Models;
using System.ComponentModel;
using System.Windows;

namespace CRUD_proj.ViewModels
{
    class SettingsViewModel : NotifiedModelBase
    {
        WindowSettings windowSettings;

        public LocalizationManager LocalizationManager { get; } = LocalizationManager.GetLocalizationManager();
        public WindowSettings WindowSettings { get => windowSettings; set { windowSettings = value; Notify(); } }
        public bool IsFullscreen
        {
            get
            {
                if(WindowSettings!=null)
                    if (WindowSettings.WindowState == WindowState.Maximized && WindowSettings.WindowStyle == WindowStyle.None)
                        return true;
                return false;
            }
            set
            {
                if (value)
                {
                    if (WindowSettings.WindowState == WindowState.Maximized)
                        windowSettings.WindowState = WindowState.Normal;
                    WindowSettings.WindowStyle = WindowStyle.None;
                    WindowSettings.WindowState = WindowState.Maximized;
                }
                else
                {
                    WindowSettings.WindowStyle = WindowStyle.ThreeDBorderWindow;
                    WindowSettings.WindowState = WindowState.Normal;
                }
            }
        }

        public SettingsViewModel()
        {
            PropertyChanged += IsFullScreenChanged;
        }

        void IsFullScreenChanged(object o, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WindowSettings")
                Notify("IsFullscreen");
        }
    }
}
