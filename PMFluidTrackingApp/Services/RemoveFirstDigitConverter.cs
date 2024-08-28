using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMFluidTrackingApp.Services;

public class RemoveFirstDigitConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string mcnumber && mcnumber.Length > 1)
        {
            return mcnumber.Substring(1);
        }
        return value; // Return the original value if it's null or too short
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
