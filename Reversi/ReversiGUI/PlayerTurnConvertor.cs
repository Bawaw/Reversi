using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReversiGUI
{
    public class PlayerTurnConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == Player.ONE)
            {
               ImageBrush imb = new ImageBrush();
               imb.ImageSource = new BitmapImage(new Uri(@"Textures\pion_white.png", UriKind.Relative));
               return imb;
            }
            if (value == Player.TWO)
            {
                ImageBrush imb = new ImageBrush();
                imb.ImageSource = new BitmapImage(new Uri(@"Textures\pion_black.png", UriKind.Relative));
                return imb;
            }
            else {
                ImageBrush imb = new ImageBrush();
                imb.ImageSource = new BitmapImage(new Uri(@"Textures\pion_black.png", UriKind.Relative));
                return imb;
            }
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("this function is not implemented");
        }
    }
}
