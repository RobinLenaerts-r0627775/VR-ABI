using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CMS
{
    public class Image
    {
        public Image(string fileName, ImageSource source)
        {
            Name = fileName;
            Code = "T1";
            Source = source;
        }

        public String Name { get; set; }

        public ImageSource Source { get; set; }
        public String Code { get; set; }
    }
}
