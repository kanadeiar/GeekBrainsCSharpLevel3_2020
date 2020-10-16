using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WpfMailSenderCore.Infrastructure.Validations
{
    public class RegexValidation : ValidationRule
    {
        private Regex _regex;
        public string Pattern
        {
            get => _regex.ToString();
            set => _regex = string.IsNullOrEmpty(value) ? null : new Regex(value);
        }
        public bool AllowNull { get; set; }
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null)
                return AllowNull
                    ? ValidationResult.ValidResult
                    : new ValidationResult(false, ErrorMessage ?? "Отсутствует значение");
            if (_regex is null)
                return ValidationResult.ValidResult;
            if (!(value is string str))
                str = value.ToString();
            return _regex.IsMatch(str)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, ErrorMessage ?? $"Значение не соответствует требованию выражения {Pattern}");
        }
    }
}
