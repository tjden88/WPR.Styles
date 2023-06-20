﻿using System;
using System.Windows;
using WPR.Controls.Base;

namespace WPR.Controls;

public class WPRNumericIntDecorator : NumericDecorator<int>
{

    static WPRNumericIntDecorator()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(WPRNumericIntDecorator), new FrameworkPropertyMetadata(typeof(WPRNumericIntDecorator)));
    }

    public WPRNumericIntDecorator() : base(1, int.MinValue, int.MaxValue)
    {
    }

    protected override void OnIncrementValueCommandExecuted() => Value = Math.Min(MaxValue, Value + Increment);

    protected override void OnDecrementValueCommandExecuted() => Value = Math.Max(MinValue, Value - Increment);

    protected override int ParseValue(string TextValue) => TextValue.ConvertToInt();

    protected override int CoerseValue(int baseValue, out string ErrorText)
    {
        ErrorText = null;

        if (baseValue < MinValue)
            ErrorText = $"Минимальное значение: {MinValue}";

        if (baseValue > MaxValue)
            ErrorText = $"Максимальное значение: {MaxValue}";

        return Math.Clamp(baseValue, MinValue, MaxValue);
    }

    protected override string SetText(int value) => value.ToString();

    protected override int CalculateFromStringExpression(string Expression, out string ErrorText)
    {
        var expressionIsValid = Expression.CalculateStringExpression(out var result, 0);

        ErrorText = expressionIsValid ? null : "Неверное выражение";

        return expressionIsValid ? (int) result : 0;
    }
}