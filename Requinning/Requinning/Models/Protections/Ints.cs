using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Ints: ProtectionBaseModel
    {
        public Ints(LoggerViewModel logger, ModuleDef Module) : base("Rename Int", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming int...");

            foreach (TypeDef type in map.Types)
            {
                if (Blacklists.Type(type) == false)
                {
                    foreach (MethodDef method in type.Methods)
                    {

                    }
                }
            }

            Logger.Record("Int renamed");
        }
    }
}
