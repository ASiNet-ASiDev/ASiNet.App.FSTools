using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ASiNet.App.FSTools._other.WinApi;
public static class FilesIcons
{


    public static async Task<ImageSource?> GetIconAsync(string fileName, CancellationToken token = default)
    {
        return await Task.Run(() =>
        {
            if (!File.Exists(fileName))
                return null;
            using var icon = Icon.ExtractAssociatedIcon(fileName);
            if (icon is null)
                return null;
            var ico = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    new Int32Rect(0, 0, icon.Width, icon.Height),
                    BitmapSizeOptions.FromEmptyOptions());
            ico.Freeze();

            return ico;
        });
    }

    public static ImageSource? GetIcon(string fileName)
    {
        if(!File.Exists(fileName))
            return null;
        using var icon = Icon.ExtractAssociatedIcon(fileName);
        if (icon is null)
            return null;
        return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    new Int32Rect(0, 0, icon.Width, icon.Height),
                    BitmapSizeOptions.FromEmptyOptions());
    }


}
