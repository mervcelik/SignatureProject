using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Signify.WPF.Extensions;

public static class ImageExtensions
{
    public static ImageSource ToImageSource(this byte[] imageData)
    {
        if (imageData == null || imageData.Length == 0)
            return null;

        using (var ms = new MemoryStream(imageData))
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = ms;
            bitmap.EndInit();
            bitmap.Freeze(); // Performans ve UI thread dışı kullanım için
            return bitmap;
        }
    }
}
