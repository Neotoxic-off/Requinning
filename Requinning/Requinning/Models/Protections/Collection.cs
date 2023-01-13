using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Collection
    {
        public Assembly Assembly { get; set; }
        public Methods Methods { get; set; }
        public Namespaces Namespaces { get; set; }
        public Properties Properties { get; set; }
        public Fields Fields { get; set; }
        public Parameters Parameters { get; set; }
        public Events Events { get; set; }
        public Strings Strings { get; set; }
        public Ints Ints { get; set; }
        public Classes Classes { get; set; }
        public Ctor Ctor { get; set; }
        public Module _Module { get; set; }

        public Collection(LoggerViewModel logger, ModuleDef Module)
        {
            Assembly = new Assembly(logger, Module);
            Methods = new Methods(logger, Module);
            Namespaces = new Namespaces(logger, Module);
            Properties = new Properties(logger, Module);
            Fields = new Fields(logger, Module);
            Parameters = new Parameters(logger, Module);
            Events = new Events(logger, Module);
            Strings = new Strings(logger, Module);
            Ints = new Ints(logger, Module);
            Classes = new Classes(logger, Module);
            Ctor = new Ctor(logger, Module);
            _Module = new Module(logger, Module);
        }
    }
}
