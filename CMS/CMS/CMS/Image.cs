using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CMS
{
    public class Image
    {
        public Image(string code, BitmapImage source)
        {
            // Temporary, could leave it empty
            Code = code;
            Source = source;
            URI = source.UriSource;
            Name = System.IO.Path.GetFileName(URI.LocalPath);
        }

        public String Name { get; set; }
        public ImageSource Source { get; set; }
        public String Code { get; set; }
        public Uri URI { get; set; }
    }
}
