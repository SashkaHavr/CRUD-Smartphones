using CRUD_proj.Models;
using DAL;

namespace CRUD_proj.ViewModels
{
    class EditViewModel : NotifiedModelBase
    {
        Smartphone smartphone;
        public LocalizationManager LocalizationManager { get; } = LocalizationManager.GetLocalizationManager();
        public Smartphone Smartphone { get => smartphone; set { smartphone = value; Notify(); } }
    }
}
