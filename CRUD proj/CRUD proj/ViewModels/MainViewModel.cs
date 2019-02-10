using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CRUD_proj.Infrastructure;
using CRUD_proj.Models;
using CRUD_proj.Models.Save_Load;
using CRUD_proj.Views;
using MaterialDesignThemes.Wpf;

namespace CRUD_proj.ViewModels
{
    class MainViewModel : NotifiedModelBase
    {
        ObservableCollection<Smartphone> smartphones;
        WindowSettings windowSettings;
        bool isSaving;
        bool isLoading;
        bool isFiltring = false;

        EditView editView = new EditView();
        Smartphone cancelEditSmartphone;
        SettingsView settingsView = new SettingsView();
        IWindowSettingsSaver settingsSaver;
        IWindowSettingsLoader settingsLoader;
        IDataSaver dataSaver;
        IDataLoader dataLoader;

        #region ICommands
        ICommand addCommand;
        ICommand deleteCommand;
        ICommand editCommand;
        ICommand settingsCommand;
        ICommand saveCommand;
        ICommand loadCommand;
        ICommand closedCommand;
        ICommand closeWindowCommand;
        ICommand filterResetCommand;
        ICommand clearAllCommand;

        public ICommand AddCommand { get => addCommand ?? (addCommand = new RelayCommand(AddCommandCall)); }
        public ICommand DeleteCommand { get => deleteCommand ?? (deleteCommand = new RelayCommand(o => Smartphones.Remove(o as Smartphone))); }
        public ICommand EditCommand { get => editCommand ?? (editCommand = new RelayCommand(EditCommandCall)); }
        public ICommand SettingsCommand { get => settingsCommand ?? (settingsCommand = new RelayCommand(SettingsCommandCall)); }
        public ICommand SaveCommand { get => saveCommand ?? (saveCommand = new RelayCommand(SaveCommandCall)); }
        public ICommand LoadCommand { get => loadCommand ?? (loadCommand = new RelayCommand(LoadCommandCall)); }
        public ICommand ClosedCommand { get => closedCommand ?? (closedCommand = new RelayCommand(ClosedCommandCall)); }
        public ICommand CloseWindowCommand { get => closeWindowCommand ?? (closeWindowCommand = new RelayCommand(o => (o as Window).Close())); }
        public ICommand FilterResetCommand { get => filterResetCommand ?? (filterResetCommand = new RelayCommand(o => Filter.Reset())); }
        public ICommand ClearAllCommand { get => clearAllCommand ?? (clearAllCommand = new RelayCommand(o => { Smartphones = new ObservableCollection<Smartphone>(); Filter.Reset(); },
                                                                                        b => Filter.Smartphones.Count != 0)); }
        #endregion

        public ObservableCollection<Smartphone> Smartphones
        {
            get => smartphones;
            set
            {
                Sorter.Smartphones = value;
                if (!isFiltring)
                {
                    Filter.Smartphones = value;
                    Filter.PropertyChanged += FilterPropertyChangedHandler;
                }
                value.CollectionChanged += CollectionChangedHandler;
                smartphones = value;
                Notify();
            }
        }
        public WindowSettings WindowSettings { get => windowSettings; set { windowSettings = value; Notify(); } }
        public LocalizationManager LocalizationManager { get; } = LocalizationManager.GetLocalizationManager();
        public bool IsLoading { get => isLoading; set { isLoading = value; Notify(); } }
        public bool IsSaving { get => isSaving; set { isSaving = value; Notify(); } }
        public SnackbarMessageQueue MessageQueue { get; } = new SnackbarMessageQueue();
        public Sorter Sorter { get; }
        public Filter Filter { get; }

        public MainViewModel(IWindowSettingsSaver settingsSaver, IWindowSettingsLoader settingsLoader, IDataSaver dataSaver, IDataLoader dataLoader)
        {
            WindowSettings = settingsLoader.Load();
            Sorter = new Sorter();
            Filter = new Filter();
            if (windowSettings.LoadOnOpening)
                Smartphones = new ObservableCollection<Smartphone>(dataLoader.Load());
            else
                Smartphones = new ObservableCollection<Smartphone>();
            this.settingsSaver = settingsSaver;
            this.settingsLoader = settingsLoader;
            this.dataSaver = dataSaver;
            this.dataLoader = dataLoader;
        }


        #region CommandCalls&EventHandlers
        void FilterPropertyChangedHandler(object o, PropertyChangedEventArgs e)
        {
            if (!Filter.Update)
            {
                isFiltring = true;
                Smartphones = new ObservableCollection<Smartphone>(Filter.GetFiltered());
                isFiltring = false;
            }
        }

        void CollectionChangedHandler(object o, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var i in e.NewItems)
                    Filter.Smartphones.Add(i as Smartphone);
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var i in e.OldItems)
                    Filter.Smartphones.Remove(i as Smartphone);
        }

        public void ClosingEventHandler(object o, CancelEventArgs e)
        {
            if ((o as MainView).host.IsOpen)
                e.Cancel = true;
        }

        public void ClosedCommandCall(object o)
        {
            if (windowSettings.SaveOnClosing)
                dataSaver.Save(Filter.Smartphones);
            settingsSaver.Save(WindowSettings);
        }

        async void AddCommandCall(object o)
        {
            Smartphone smartphone = new Smartphone();
            (editView.DataContext as ViewModelLocator).EditViewModel.Smartphone = smartphone;
            await DialogHost.Show(editView, DialogOpening, EditClosing);
            if ((editView.DataContext as ViewModelLocator).EditViewModel.Smartphone != null)
            {
                Smartphones.Add(smartphone);
                Sorter.Sort();
            }
        }

        async void EditCommandCall(object o)
        {
            cancelEditSmartphone = (o as Smartphone).Clone() as Smartphone;
            (editView.DataContext as ViewModelLocator).EditViewModel.Smartphone = o as Smartphone;
            await DialogHost.Show(editView, DialogOpening, EditClosing);
            if ((editView.DataContext as ViewModelLocator).EditViewModel.Smartphone == null)
            {
                int i = Smartphones.IndexOf(o as Smartphone);
                Smartphones[i] = cancelEditSmartphone;
                i = Filter.Smartphones.IndexOf(o as Smartphone);
                Filter.Smartphones[i] = cancelEditSmartphone;
            }
            else
            {
                Sorter.Sort();
                Filter.EditFilterUpdate(o as Smartphone, cancelEditSmartphone);
            }
        }

        async void SettingsCommandCall(object o)
        {
            (settingsView.DataContext as ViewModelLocator).SettingsViewModel.WindowSettings = WindowSettings;
            await DialogHost.Show(settingsView, DialogOpening, SettingsClosing);
        }

        void SaveCommandCall(object o)
        {
            Task task = new Task(() => dataSaver.Save(Filter.Smartphones));
            task.ContinueWith(t => IsSaving = false);
            task.ContinueWith(t => MessageQueue.Enqueue("Saved"));
            IsSaving = true;
            task.Start();
        }

        void LoadCommandCall(object o)
        {
            Task task = new Task(() => Smartphones = new ObservableCollection<Smartphone>(dataLoader.Load()));
            task.ContinueWith(t => IsLoading = false);
            task.ContinueWith(t => MessageQueue.Enqueue("Loaded"));
            IsLoading = true;
            task.Start();
        }

        #endregion

        #region Dialogs
        void DialogOpening(object o, DialogOpenedEventArgs e)
        {
            (o as DialogHost).DialogTheme = BaseTheme.Inherit;
        }

        void SettingsClosing(object o, DialogClosingEventArgs e)
        {
            if(e.Parameter!=null)
            {
                string str = e.Parameter.ToString();
                if (str == "Save")
                    settingsSaver.Save(WindowSettings);
                else if (str == "Cancel")
                    WindowSettings = settingsLoader.Load();
            }
        }

        void EditClosing(object o, DialogClosingEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.ToString() == "Cancel")
                (editView.DataContext as ViewModelLocator).EditViewModel.Smartphone = null;
            else if (!IsValid(editView))
                e.Cancel();
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
                return false;
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
                if (!IsValid(VisualTreeHelper.GetChild(parent, i)))
                    return false;
            return true;
        }
        #endregion
    }
}
