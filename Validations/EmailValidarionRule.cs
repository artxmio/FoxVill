using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace FoxVill.Validation;

public class EmailValidationRule : ValidationRule
{
    private static readonly Regex EmailRegex = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]+$", RegexOptions.Compiled);

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var email = value as string;
        if (string.IsNullOrWhiteSpace(email))
        {
            return new ValidationResult(false, "Введите адрес электронной почты.");
        }
        if (!EmailRegex.IsMatch(email))
        {
            return new ValidationResult(false, "Почта не должна содержать следующие символы: пробелы,\nспециальные символы и более одного символа @.");
        }
        return ValidationResult.ValidResult;
    }
}