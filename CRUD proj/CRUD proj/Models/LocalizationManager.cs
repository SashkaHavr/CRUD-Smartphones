using DAL;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Globalization;
using System.ComponentModel;

namespace CRUD_proj.Models
{
    class LocalizationManager : NotifiedModelBase
    {
        static LocalizationManager localizationManager;
        public static LocalizationManager GetLocalizationManager() => localizationManager == null ? localizationManager = new LocalizationManager() : localizationManager;
        ResourceManager resourceManager = new ResourceManager("CRUD_proj.Resources.Strings", typeof(LocalizationManager).Assembly);

        public string CurrentCulture
        {
            set
            {
                CultureInfo cultureInfo = new CultureInfo(value);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Notify();
            }
        }
        public List<string> AvailibleLanguages { get; } = new List<string>() { "ru-RU", "en-US" };

        public string SaveHint { get => GetString(); }
        public string LoadHint { get => GetString(); }
        public string SettingsHint { get => GetString(); }
        public string ThemeHint { get => GetString(); }
        public string FiltersText { get => GetString(); }
        public string SortHint { get => GetString(); }
        public string EditHint { get => GetString(); }
        public string DeleteHint { get => GetString(); }
        public string ManufacturerTitle { get => GetString(); }
        public string DisplaySizeTitle { get => GetString(); }
        public string DisplayResolutionTitle { get => GetString(); }
        public string RamTitle { get => GetString(); }
        public string RomTitle { get => GetString(); }
        public string CpuTypeTitle { get => GetString(); }
        public string CpuSpeedTitle { get => GetString(); }
        public string RearCameraResolutionTitle { get => GetString(); }
        public string FrontCameraResolutionTitle { get => GetString(); }
        public string NfcTitle { get => GetString(); }
        public string JackTitle { get => GetString(); }
        public string OsTitle { get => GetString(); }
        public string BatteryCapacityTitle { get => GetString(); }
        public string ConnectionTitle { get => GetString(); }
        public string AddHint { get => GetString(); }
        public string DescriptionTitle { get => GetString(); }
        public string TitleTitle { get => GetString(); }
        public string PriceTitle { get => GetString(); }
        public string PicturePathTitle { get => GetString(); }
        public string EmptyValidError { get => GetString(); }
        public string WrongValueError { get => GetString(); }
        public string LinkError { get => GetString(); }
        public string TrueText { get => GetString(); }
        public string FalseText { get => GetString(); }
        public string SaveBtnText { get => GetString(); }
        public string CancelBtnText { get => GetString(); }
        public string FullScreenText { get => GetString(); }
        public string ImageSizeText { get => GetString(); }
        public string LanguageHint { get => GetString(); }
        public string SmartphonesTitle { get => GetString(); }
        public string LoadOnOpeningText { get => GetString(); }
        public string SaveOnClosingText { get => GetString(); }
        public string DescendingText { get => GetString(); }
        public string PriceComparerText { get => GetString(); }
        public string TitleComparerText { get => GetString(); }
        public string BatteryCapacityComparerText { get => GetString(); }
        public string FromHint { get => GetString(); }
        public string ToHint { get => GetString(); }
        public string ResetHint { get => GetString(); }
        public string ClearAllHint { get => GetString(); }
        public string SearchHint { get => GetString(); }
        public string ManufacturerComparerText { get => GetString(); }

        LocalizationManager()
        {
            PropertyChanged += PropertyChangedHandler;
        }

        string GetString([CallerMemberName]string str = "")
        {
            try { return resourceManager.GetString(str); }
            catch { return string.Empty; }
        }

        void PropertyChangedHandler(object o, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CurrentCulture")
            {
                foreach (var prop in typeof(LocalizationManager).GetProperties())
                    if (prop.Name != "CurrentCulture" && prop.Name != "AvailibleLanguages")
                        Notify(prop.Name);
            }
        }
    }
}
