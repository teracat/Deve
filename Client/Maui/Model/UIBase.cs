﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Deve.ClientApp.Maui
{
    public class UIBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected virtual bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            bool changed = false;
            if (EqualityComparer<T>.Default.Equals(property, default))
                changed = !EqualityComparer<T>.Default.Equals(value, default);
            else if (property is not null)
                changed = !property.Equals(value);
            if (changed)
            {
                property = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }
}
