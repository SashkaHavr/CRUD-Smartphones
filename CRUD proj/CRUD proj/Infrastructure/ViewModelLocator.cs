using Ninject;
using CRUD_proj.ViewModels;
using CRUD_proj.Infrastructure.Modules;

namespace CRUD_proj.Infrastructure
{
    class ViewModelLocator
    {
        StandardKernel kernel;
        MainViewModel mainViewModel;
        EditViewModel editViewModel;
        SettingsViewModel settingsViewModel;

        public MainViewModel MainViewModel { get => mainViewModel ?? (mainViewModel = kernel.Get<MainViewModel>()); }
        public EditViewModel EditViewModel { get => editViewModel ?? (editViewModel = kernel.Get<EditViewModel>()); }
        public SettingsViewModel SettingsViewModel { get => settingsViewModel ?? (settingsViewModel = kernel.Get<SettingsViewModel>()); }

        public ViewModelLocator()
        {
            kernel = new StandardKernel(new MainViewModelModule(),
                                        new EditViewModelModule(),
                                        new SettingsViewModelModule());
        }
    }
}
