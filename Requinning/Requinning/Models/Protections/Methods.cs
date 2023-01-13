using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Methods: ProtectionBaseModel
    {
        public Methods(LoggerViewModel logger, ModuleDef Module) : base("Rename Methods", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming methods...");

            foreach (TypeDef type in map.Types)
            {
                if (Blacklists.Type(type) == false)
                {
                    foreach (MethodDef method in type.Methods)
                    {
                        if (Blacklists.Method(method) == false)
                        {
                            method.Name = Rename(method.Name);
                        }
                    }
                }
            }

            Logger.Record("Methods renamed");
        }
    }
}
