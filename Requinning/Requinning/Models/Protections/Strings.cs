using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Strings: ProtectionBaseModel
    {
        public Strings(LoggerViewModel logger, ModuleDef Module) : base("Rename Strings", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming strings...");

            foreach (TypeDef type in map.Types)
            {
                if (Blacklists.Type(type) == false)
                {
                    foreach (MethodDef method in type.Methods)
                    {
                    }
                }
            }

            Logger.Record("Strings renamed");
        }
    }
}
