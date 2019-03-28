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
        public Image(string fileName, BitmapImage source)
        {
            Name = fileName;
            // Temporary, could leave it empty
            Code = "T1";
            Source = source;
            URI = source.UriSource;
        }

        public String Name { get; set; }

        public ImageSource Source { get; set; }
        public String Code { get; set; }
        public Uri URI { get; set; }
        
    }
}
