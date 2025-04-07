using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
namespace FoxVill.Validation;

public class PasswordValidationRule : ValidationRule
{
    private static readonly Regex PasswordRegex = new Regex("^[A-Za-z\\d@$!%*?&]{5,16}$", RegexOptions.Compiled);

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var password = value as string;
        if (string.IsNullOrWhiteSpace(password))
        {
            return new ValidationResult(false, "Введите пароль.");
        }
        if (!PasswordRegex.IsMatch(password))
        {
            return new ValidationResult(false, "Длина пароля должна быть более 5 и менее 16 символов.\nПароль может содержать следующие символы: буквы (A-z, a-z), специальные символы: @, $, !, %, *, ?, &");
        }
        return ValidationResult.ValidResult;
    }
}