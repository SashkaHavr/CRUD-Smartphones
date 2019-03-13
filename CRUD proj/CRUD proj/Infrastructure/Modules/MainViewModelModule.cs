using CRUD_proj.Models.Save_Load;
using Ninject.Modules;

namespace CRUD_proj.Infrastructure.Modules
{
    class MainViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWindowSettingsLoader>().To<JSOMWindowSettingsLoader>();
            Bind<IWindowSettingsSaver>().To<JSONWindoSettingsSaver>();
            Bind<ISmartphoonesRepository>().To<SqlLiteSmartphonesRepositoryAdapter>();
        }
    }
}
