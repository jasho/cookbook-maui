using System.Globalization;

namespace CookBook.Mobile.Converters
{
    public class LocalImageWithPlaceholderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result;

            var valueString = value as string;
            var placeholder = parameter as string ?? string.Empty;

#if WINDOWS
            if(valueString is null)
            {
                result = placeholder.Split('/').Last();
            }
            else
            {
                result = valueString.Split('/').Last();
            }
#else
            result = valueString ?? placeholder;
#endif

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}