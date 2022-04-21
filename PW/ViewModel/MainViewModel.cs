using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <param name="propertyName">(optional) The name of the property that changed.
        /// The <see cref="CallerMemberName"/> allows you to obtain the method or property name of the caller to the method.
        protected virtual void RaisePropertyChange([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
        }
    }
}
