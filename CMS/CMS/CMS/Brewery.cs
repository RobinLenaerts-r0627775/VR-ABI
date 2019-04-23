using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS
{
    public class Brewery
    {
        public Brewery(string name)
        {
            Name = name;
            List = new List<Image>();
        }

        public string Name { get; set; }
        public List<Image> List { get; set; }
    }
}
