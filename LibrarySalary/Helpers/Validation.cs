using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Salary.Helpers
{
    public class StringLengthValidation : ValidationRule
    {
        public StringLengthValidation()
        {
            MinLength = int.MinValue;
            MaxLength = int.MaxValue;
        }
        public int MinLength
        {
            get;
            set;
        }
        public int MaxLength
        {
            get;
            set;
        }
        public string ErrorMessage
        {
            get;
            set;
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null && value != DependencyProperty.UnsetValue && !string.IsNullOrEmpty(value.ToString()))
            {
                string s = value as string;
                if (s.Length >= MinLength && s.Length <= MaxLength)
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, string.IsNullOrEmpty(ErrorMessage) ? string.Format("Длина поля должна быть от {0} до {1} символов", MinLength, MaxLength) : ErrorMessage);
            }
            else
                return ValidationResult.ValidResult;
        }
    }

    public class CharDigitValueValidation : ValidationRule
    {
        public CharDigitValueValidation()
        {
        }
        public string ErrorMessage
        {
            get;
            set;
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                string s = value as string;
                if (s.Any(t => !char.IsDigit(t)))
                    return new ValidationResult(false, string.IsNullOrEmpty(ErrorMessage) ? "Поле может содержать только цифры" : ErrorMessage);
                return ValidationResult.ValidResult;
            }
            else
                return ValidationResult.ValidResult;
        }
    }

    public class ValidationHelper
    {
        public static bool IsFrameworkElementsHasErrors(DependencyObject[] e)
        {
            return !Array.TrueForAll<DependencyObject>(e, t => !IsElementHasErrors(t));
        }
        public static bool IsElementHasErrors(DependencyObject e)
        {
            if (e == null) return false;
            else
                if (Validation.GetHasError(e))
                    return true;
                else
                {
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(e); ++i)
                    {
                        if (IsElementHasErrors(VisualTreeHelper.GetChild(e, i)))
                            return true;
                    }
                }
            return false;
        }

        public static ValidationError GetError(DependencyObject e)
        {
            if (e == null) return null;
            else
                if (Validation.GetHasError(e))
                    return Validation.GetErrors(e)[0];
                else
                {
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(e); ++i)
                    {
                        ValidationError rr = GetError(VisualTreeHelper.GetChild(e, i));
                        if (rr != null)
                            return rr;
                    }
                }
            return null;
        }
    }

}
