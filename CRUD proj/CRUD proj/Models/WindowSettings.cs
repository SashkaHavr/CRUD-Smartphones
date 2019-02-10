using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CRUD_proj.Models
{
    class WindowSettings : NotifiedModelBase
    {
        PaletteHelper paletteHelper = new PaletteHelper();
        bool isDark = false;
        string culture = Thread.CurrentThread.CurrentUICulture.Name;
        int imageHeight = 100;
        int cardSize = 190;
        Resolution windowResolution = new Resolution() { Horizontal = 520, Vertical = 400 };
        WindowState windowState = WindowState.Normal;
        WindowStyle windowStyle = WindowStyle.ThreeDBorderWindow;
        bool loadOnOpening = true;
        bool saveOnClosing = true;

        public bool IsDark { get => isDark; set { isDark = value; Notify(); } }
        public string Culture { get => culture; set { culture = value; LocalizationManager.GetLocalizationManager().CurrentCulture = value; Notify(); } }
        public int ImageHeight { get => imageHeight; set { imageHeight = value; Notify(); } }
        public int CardSize { get => cardSize; set { cardSize = value; Notify(); } }
        public Resolution WindowResolution { get => windowResolution; set { windowResolution = value; Notify(); } }
        public WindowState WindowState { get => windowState; set { windowState = value; Notify(); } }
        public WindowStyle WindowStyle { get => windowStyle; set { windowStyle = value; Notify(); } }
        public bool LoadOnOpening { get => loadOnOpening; set { loadOnOpening = value; Notify(); } }
        public bool SaveOnClosing { get => saveOnClosing; set { saveOnClosing = value; Notify(); } }

        public WindowSettings()
        {
            PropertyChanged += PropertyChangedHandler;
        }

        void PropertyChangedHandler(object o, PropertyChangedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage();
            if (e.PropertyName == "IsDark")
                paletteHelper.SetLightDark(isDark);
            else if (e.PropertyName == "ImageHeight")
                    CardSize = imageHeight + 90;
        }
    }
}
