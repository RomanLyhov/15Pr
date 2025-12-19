using System.Globalization;
using System.Windows.Controls;

namespace Pract15.Validstors
{
    internal class RatingRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.ValidResult;

            string str = value.ToString();

            // Разрешаем частичный ввод (например, "2.", "3,")
            // Если строка заканчивается на точку или запятую, удаляем этот символ для проверки
            if (str.EndsWith(".") || str.EndsWith(","))
            {
                str = str.Substring(0, str.Length - 1);
            }

            // Если после удаления символа строка пуста, это допустимо
            if (string.IsNullOrEmpty(str))
                return ValidationResult.ValidResult;

            // Пробуем распарсить число
            string normalizedStr = str.Replace(',', '.');

            if (!double.TryParse(
                normalizedStr,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double rating))
            {
                return new ValidationResult(false, "Введите число от 0 до 5");
            }

            if (rating < 0 || rating > 5)
                return new ValidationResult(false, "Рейтинг должен быть от 0 до 5");

            return ValidationResult.ValidResult;
        }
    }
}