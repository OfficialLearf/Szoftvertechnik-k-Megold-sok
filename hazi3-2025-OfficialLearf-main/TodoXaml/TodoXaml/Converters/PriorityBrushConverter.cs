using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoXaml.Models;
using Windows.UI;

namespace TodoXaml.Converters
{
    public class PriorityBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Priority priority)
            {
                switch (priority)
                {
                    case Priority.High:
                        return new SolidColorBrush(Colors.Red); 
                    case Priority.Normal:
                        return new SolidColorBrush(Colors.Black); 
                    case Priority.Low:
                        return new SolidColorBrush(Colors.Blue); 
                }
            }
            return new SolidColorBrush(Colors.Black); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
