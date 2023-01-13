using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models
{
    public class ThemeModel
    {
        public class Root
        {
            public string selected { get; set; }
            public List<Theme> themes { get; set; }
        }

        public class Theme
        {
            public string name { get; set; }
            public string Background { get; set; }
            public string ContentBackground { get; set; }
            public string Normal { get; set; }
            public string Title { get; set; }
            public string Action { get; set; }
            public string Hover { get; set; }
            public string Disabled { get; set; }
        }

    }
}
