﻿using System;
using System.Windows;
using System.Windows.Media;
using WPR.Models.Themes;
using WPR.MVVM.Converters;
using WPR.MVVM.Converters.Base;

namespace WPR.ColorTheme;

/// <summary>
/// Установка и изменение цветовой темы
/// </summary>
public static class StyleHelper
{
    private static readonly TypeConverter<SolidColorBrush> _BrushLightOrDarkConverter = new(new BrushLightOrDarkConverter());

    /// <summary> Цвета текущей сессии </summary>
    public static readonly StyleColors StyleColors = (StyleColors)Application.Current.Resources["StyleColors"];
    private static Color DarkColor => StyleColors.DarkColor; // Кисть тёмной темы
    private static Color WhiteColor => StyleColors.LightColor; // Кисть светлой темы


    /// <summary> Происходит при любом изменении цветовой схемы</summary>
    public static event EventHandler StyleChanged;


    /// <summary>Установлена ли тёмная тема</summary>
    public static bool IsDarkTheme => StyleColors.DarkColor == StyleColors.BackgroundColor;


    /// <summary>Задать новый рандомный стиль (цветовую палитру) элементам управления</summary>
    public static void SetNewRandomStyle()
    {
        Random rnd = new();
        var rndColor = Color.FromRgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255));
        SetPrimaryColor(rndColor);
        SetAccentColor(Color.FromRgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
    }


    /// <summary>Установить главный цвет (включая тёмный и светлый)</summary>
    public static void SetPrimaryColor(Color color)
    {
        if (StyleColors.LightWindowBackgroundColor == StyleColors.PrimaryColor)
            StyleColors.LightWindowBackgroundColor = color;

        StyleColors.PrimaryColor = color;
        StyleColors.DarkPrimaryColor = Darken(color, 1.2);
        StyleColors.LightPrimaryColor = Lighten(color, 1.5);

        SetWindowColors(IsDarkTheme);
        StyleChanged?.Invoke(null, EventArgs.Empty);
    }


    /// <summary>Установить цвет акцента</summary>
    public static void SetAccentColor(Color color)
    {
        StyleColors.AccentColor = color;
        StyleChanged?.Invoke(null, EventArgs.Empty);
    }



    /// <summary>Найти кисть в ресурсах</summary>
    /// <param name="BrushName">Имя кисти</param>
    public static SolidColorBrush GetBrushFromResource(StyleBrushes BrushName)
    {
        var br = (SolidColorBrush)Application.Current.Resources[BrushName.ToString()];
        return br;
    }


    #region Theme

    /// <summary>
    /// Установить тёмную тему.
    /// </summary>
    public static void SetDarkColorTheme()
    {
        StyleColors.BackgroundColor = DarkColor;
        StyleColors.SecondaryBackgroundColor = Lighten(DarkColor, 10);
        StyleColors.TextColor = WhiteColor;
        StyleColors.ShadowColor = Colors.Black;
        StyleColors.DividerColor = Lighten(StyleColors.DarkColor, 5);
        StyleColors.ContrastColor = (Color)ColorConverter.ConvertFromString("#c8c8c8")!;

        SetWindowColors(true);
        StyleChanged?.Invoke(null, EventArgs.Empty);
    }

    /// <summary>
    /// Установить светлую тему.
    /// </summary>
    public static void SetLightColorTheme()
    {
        StyleColors.BackgroundColor = WhiteColor;
        StyleColors.SecondaryBackgroundColor = WhiteColor;
        StyleColors.TextColor = DarkColor;
        StyleColors.ShadowColor = Colors.DimGray;
        StyleColors.DividerColor = (Color)ColorConverter.ConvertFromString("#FFE0E0E0")!;
        StyleColors.ContrastColor = DarkColor;


        SetWindowColors(false);
        StyleChanged?.Invoke(null, EventArgs.Empty);
    }

    #endregion


    #region Private

    private static void SetWindowColors(bool isDarkTheme)
    {
        var windowBackgroundColor = isDarkTheme
        ? StyleColors.DarkWindowBackgroundColor
        : StyleColors.LightWindowBackgroundColor;

        StyleColors.WindowBackgroundColor = windowBackgroundColor;
        StyleColors.InactiveWindowBackgroundColor = Lighten(windowBackgroundColor, isDarkTheme ? 5 : 2);

        var foregroungBrush = _BrushLightOrDarkConverter.Convert(new(windowBackgroundColor));
        StyleColors.WindowForegroundColor = foregroungBrush.Color;
    }


    /// <summary>Взять цвет светлее</summary>
    private static Color Lighten(Color basic, double koef)
    {
        var lighten = Color.FromArgb(255, (byte)(basic.R + (255 - basic.R) / koef),
            (byte)(basic.G + (255 - basic.G) / koef),
            (byte)(basic.B + (255 - basic.B) / koef));
        return lighten;
    }

    /// <summary>Взять цвет темнее</summary>
    private static Color Darken(Color basic, double koef)
    {
        var darken = Color.FromArgb(255, (byte)(basic.R / koef),
            (byte)(basic.G / koef),
            (byte)(basic.B / koef));
        return darken;
    }

    #endregion
}