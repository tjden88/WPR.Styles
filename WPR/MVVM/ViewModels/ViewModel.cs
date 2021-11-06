﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WPR.MVVM.ViewModels
{
    public abstract partial class ViewModel : INotifyPropertyChanged
    {
        /// <summary>Признак того, что мы находимся в режиме разработки под Visual Studio</summary>
        public static bool IsDesignMode => Application.Current.MainWindow != null && DesignerProperties.GetIsInDesignMode(Application.Current.MainWindow);
        //public static bool IsDesignMode => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
        
        protected ValueResult<T> IfSet<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            var res = Set(ref field, value, PropertyName);
            return new ValueResult<T>(res, value, this);
        }

        /// <summary> Обновить все свойства ViewModel </summary>
        public virtual void OnAllPropertiesChanged()
        {
            foreach (var propertyInfo in GetType().GetProperties())
            {
                OnPropertyChanged(propertyInfo.Name);
            }
        }
    }
}
