using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Namespaces: ProtectionBaseModel
    {
        public Namespaces(LoggerViewModel logger, ModuleDef Module) : base("Rename Namespaces", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming namespaces...");

            foreach (TypeDef type in map.Types)
            {
                if (Blacklists.Type(type) == false)
                {
                    type.Namespace = Rename(type.Namespace);
                }
            }

            Logger.Record("Namespaces renamed");
        }
    }
}
