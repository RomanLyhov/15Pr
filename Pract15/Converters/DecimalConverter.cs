using System;
using System.Globalization;
using System.Windows.Data;

namespace Pract15.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            return ((decimal?)value)?.ToString(culture) ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string ?? "";
            str = str.Trim();

            if (string.IsNullOrEmpty(str))
                return null;

          
            if (str.EndsWith(".") || str.EndsWith(","))
            {
               
                string withoutSeparator = str.Substring(0, str.Length - 1);

                
                if (decimal.TryParse(withoutSeparator, NumberStyles.Any, culture, out _) ||
                    decimal.TryParse(withoutSeparator, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                {
                   
                    return null;
                }
            }


            decimal result;


            if (decimal.TryParse(str, NumberStyles.Any, culture, out result))
                return result;

            if (decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;
            string strWithDot = str.Replace(',', '.');
            if (decimal.TryParse(strWithDot, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;

            string strWithComma = str.Replace('.', ',');
            if (decimal.TryParse(strWithComma, NumberStyles.Any, culture, out result))
                return result;

            return null;
        }
    }
}