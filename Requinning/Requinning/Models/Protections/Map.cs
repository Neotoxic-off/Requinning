using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Map
    {
        public Dictionary<string, string> Renamed { get; set; }
        public IEnumerable<TypeDef> Types { get; set; }

        public Map()
        {
            Renamed = new Dictionary<string, string>();
            Types = new List<TypeDef>();
        }
    }
}
