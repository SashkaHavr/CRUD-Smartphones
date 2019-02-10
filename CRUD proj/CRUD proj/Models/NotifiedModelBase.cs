using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRUD_proj.Models
{
    abstract class NotifiedModelBase : INotifyPropertyChanged
    {
        protected void Notify([CallerMemberName]string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
