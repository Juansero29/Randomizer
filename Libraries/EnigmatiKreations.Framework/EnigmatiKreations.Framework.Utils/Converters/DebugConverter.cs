using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Utils.Converters
{
    /// <summary>
    /// Converter qui ne fait rien. A utiliser pendant le dev quand on arrive pas a faire le binding pour voir ce qu'il y a dans value
    /// </summary>
    /// <remarks></remarks>
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Element elt;
            elt = value as Element;
            if (elt != null)
            {
                var p = elt.Parent;
                if (elt != null && elt.BindingContext != null)
                {

                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
