﻿using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPR.Demo.Pages
{
    /// <summary>
    /// Логика взаимодействия для Fields.xaml
    /// </summary>
    public partial class Fields : Page
    {
        public Fields()
        {
            InitializeComponent();
        }

    }


    public class AgeRangeRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public AgeRangeRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((age < Min) || (age > Max))
            {
                return new ValidationResult(false,
                    $"Please enter an age in the range: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}