using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DAL
{
    public abstract class NotifiedModelBase : INotifyPropertyChanged
    {
        protected void Notify([CallerMemberName]string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
