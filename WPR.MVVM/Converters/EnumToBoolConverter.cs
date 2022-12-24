﻿using System.Globalization;
using System.Windows.Data;
using WPR.MVVM.Converters.Base;

namespace WPR.MVVM.Converters
{
    /// <summary>
    /// Конвертер enum в bool
    /// </summary>
    public class EnumToBoolConverter : Converter
    {
        protected override object Convert(object v, Type t, object p, CultureInfo c) => v.Equals(p);

        protected override object ConvertBack(object v, Type t, object p, CultureInfo c) => v.Equals(true) ? p : Binding.DoNothing;
    }
}
